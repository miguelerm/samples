using Samples.CacheSample.Entities;

namespace Samples.CacheSample.Repositories {
    public class BooksSqLiteRepository : IBooksRepository
    {
        public int Add(string title, string author, int year)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Book[] GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Book GetSingle(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(int id, string title, string author, int year)
        {
            throw new System.NotImplementedException();
        }
    }
}