using System.Threading.Tasks;
using Samples.CacheSample.Entities;

namespace Samples.CacheSample.Repositories
{
    public interface IBooksRepository
    {
        int Add(string title, string author, int year);
        bool Update(int id, string title, string author, int year);
        bool Delete(int id);
        Book[] GetAll();
        Book GetSingle(int id);
        Task<int> AddAsync(string title, string author, int year);
        Task<bool> UpdateAsync(int id, string title, string author, int year);
        Task<bool> DeleteAsync(int id);
        Task<Book[]> GetAllAsync();
        Task<Book> GetSingleAsync(int id);
    }

}