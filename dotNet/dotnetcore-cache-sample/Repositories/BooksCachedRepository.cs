using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Samples.CacheSample.Entities;

namespace Samples.CacheSample.Repositories
{
    public class BooksCachedRepository : IBooksRepository
    {
        private readonly IBooksRepository repository;
        private readonly IMemoryCache cache;
        private readonly ILogger<BooksCachedRepository> logger;
        private const string GetAllCacheKey = "BOOKS_ALL";
        private const string GetSingleCacheKeyPrefix = "BOOKS_";

        public BooksCachedRepository(IBooksRepository repository, IMemoryCache cache, ILogger<BooksCachedRepository> logger)
        {
            this.repository = repository;
            this.cache = cache;
            this.logger = logger;
        }

        public int Add(string title, string author, int year)
        {
            var id = repository.Add(title, author, year);
            if (id > 0)
            {
                logger.LogDebug($"Clean cache: {GetAllCacheKey}");
                cache.Remove(GetAllCacheKey);
            }

            return id;
        }

        public bool Delete(int id)
        {
            var deleted = repository.Delete(id);

            if (deleted)
            {
                logger.LogDebug($"Clean cache: {GetAllCacheKey} and {GetSingleCacheKeyPrefix + id}");
                cache.Remove(GetAllCacheKey);
                cache.Remove(GetSingleCacheKeyPrefix + id);
            }

            return deleted;
        }

        public Book[] GetAll()
        {
            var cachedResult = cache.Get(GetAllCacheKey) as Book[];
            if (cachedResult != null)
            {
                logger.LogDebug("Getting all books from cache");
                return cachedResult;
            }

            var result = repository.GetAll();
            cache.Set(GetAllCacheKey, result);
            return result;
        }

        public Book GetSingle(int id)
        {
            var cacheKey = GetSingleCacheKeyPrefix + id;
            var cachedResult = cache.Get(cacheKey) as Book;
            if (cachedResult != null)
            {
                logger.LogDebug($"Getting book {id} from cache");
                return cachedResult;
            }

            var result = repository.GetSingle(id);
            cache.Set(cacheKey, result);
            return result;
        }

        public bool Update(int id, string title, string author, int year)
        {
            var updated = repository.Update(id, title, author, year);

            if (updated)
            {
                logger.LogDebug($"Clean cache: {GetAllCacheKey} and {GetSingleCacheKeyPrefix + id}");
                cache.Remove(GetAllCacheKey);
                cache.Remove(GetSingleCacheKeyPrefix + id);
            }

            return updated;
        }

    }
}