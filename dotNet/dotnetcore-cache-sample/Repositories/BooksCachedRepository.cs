using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Samples.CacheSample.Entities;

namespace Samples.CacheSample.Repositories {
    public class BooksCachedRepository: IBooksRepository
    {
        private readonly IBooksRepository repository;
        private readonly IMemoryCache cache;

        private const string GetAllCacheKey = "BOOKS_GETALL";
        private const string GetSingleCacheKeyPrefix = "BOOKS_GETSINGLE_";

        public BooksCachedRepository(IBooksRepository repository, IMemoryCache cache)
        {
            this.repository = repository;
            this.cache = cache;
        }

        public int Add(string title, string author, int year)
        {
            var id = repository.Add(title, author, year);
            if (id > 0) {
                cache.Remove(GetAllCacheKey);
            }

            return id;
        }

        public bool Delete(int id)
        {
            var deleted = repository.Delete(id);

            if (deleted) {
                cache.Remove(GetAllCacheKey);
                cache.Remove(GetSingleCacheKeyPrefix + id);
            }

            return deleted;
        }

        public Book[] GetAll()
        {
            var cachedResult = cache.Get(GetAllCacheKey) as Book[];
            if (cachedResult != null) {
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
            if (cachedResult != null) {
                return cachedResult;
            }

            var result = repository.GetSingle(id);
            cache.Set(cacheKey, result);
            return result;
        }

        public bool Update(int id, string title, string author, int year)
        {
            var updated = repository.Update(id, title, author, year);

            if (updated) {
                cache.Remove(GetAllCacheKey);
                cache.Remove(GetSingleCacheKeyPrefix + id);
            }

            return updated;
        }

        public async Task<int> AddAsync(string title, string author, int year)
        {
            var id = await repository.AddAsync(title, author, year);
            if (id > 0) {
                cache.Remove(GetAllCacheKey);
            }

            return id;
        }

        public async Task<bool> UpdateAsync(int id, string title, string author, int year)
        {
            var updated = await repository.UpdateAsync(id, title, author, year);

            if (updated) {
                cache.Remove(GetAllCacheKey);
                cache.Remove(GetSingleCacheKeyPrefix + id);
            }

            return updated;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await repository.DeleteAsync(id);

            if (deleted) {
                cache.Remove(GetAllCacheKey);
                cache.Remove(GetSingleCacheKeyPrefix + id);
            }

            return deleted;
        }

        public async Task<Book[]> GetAllAsync()
        {
            var cachedResult = cache.Get(GetAllCacheKey) as Book[];
            if (cachedResult != null) {
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
            if (cachedResult != null) {
                return cachedResult;
            }

            var result = await repository.GetSingleAsync(id);
            cache.Set(cacheKey, result);
            return result;
        }
    }
}