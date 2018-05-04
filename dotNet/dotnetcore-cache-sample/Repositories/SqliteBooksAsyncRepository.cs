using System.Threading.Tasks;
using Samples.CacheSample.Entities;

namespace Samples.CacheSample.Repositories {
    public class BooksSqliteAsyncRepository : IBooksAsyncRepository
    {
        public Task<int> AddAsync(string title, string author, int year)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Book[]> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Book> GetSingleAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, string title, string author, int year)
        {
            throw new System.NotImplementedException();
        }
    }
}