using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Samples.CacheSample.Entities;

namespace Samples.CacheSample.Repositories {
    public class BooksCachedAsyncRepository: IBooksAsyncRepository {
        private readonly IBooksAsyncRepository repository;
        private readonly IMemoryCache cache;
        private readonly ILogger<BooksCachedAsyncRepository> logger;
        private const string GetAllCacheKey = "BOOKS_ALL";
        private const string GetSingleCacheKeyPrefix = "BOOKS_";

        public BooksCachedAsyncRepository(IBooksAsyncRepository repository, IMemoryCache cache, ILogger<BooksCachedAsyncRepository> logger)
        {
            this.repository = repository;
            this.cache = cache;
            this.logger = logger;
        }

        public async Task<int> AddAsync(string title, string author, int year)
        {
            var id = await repository.AddAsync(title, author, year);
            if (id > 0)
            {
                logger.LogDebug($"Clean cache: {GetAllCacheKey}");
                cache.Remove(GetAllCacheKey);
            }

            return id;
        }

        public async Task<bool> UpdateAsync(int id, string title, string author, int year)
        {
            var updated = await repository.UpdateAsync(id, title, author, year);

            if (updated)
            {
                logger.LogDebug($"Clean cache: {GetAllCacheKey} and {GetSingleCacheKeyPrefix + id}");
                cache.Remove(GetAllCacheKey);
                cache.Remove(GetSingleCacheKeyPrefix + id);
            }

            return updated;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await repository.DeleteAsync(id);

            if (deleted)
            {
                logger.LogDebug($"Clean cache: {GetAllCacheKey} and {GetSingleCacheKeyPrefix + id}");
                cache.Remove(GetAllCacheKey);
                cache.Remove(GetSingleCacheKeyPrefix + id);
            }

            return deleted;
        }

        public async Task<Book[]> GetAllAsync()
        {
            var cachedResult = cache.Get(GetAllCacheKey) as Book[];
            if (cachedResult != null)
            {
                logger.LogDebug("Getting all books from cache");
                return cachedResult;
            }

            var result = await repository.GetAllAsync();
            cache.Set(GetAllCacheKey, result);
            return result;
        }

        public async Task<Book> GetSingleAsync(int id)
        {
            var cacheKey = GetSingleCacheKeyPrefix + id;
            var cachedResult = cache.Get(cacheKey) as Book;
            if (cachedResult != null)
            {
                logger.LogDebug($"Getting book {id} from cache");
                return cachedResult;
            }

            var result = await repository.GetSingleAsync(id);
            cache.Set(cacheKey, result);
            return result;
        }
    }
}