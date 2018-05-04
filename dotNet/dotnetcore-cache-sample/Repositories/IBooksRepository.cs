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
    }

}