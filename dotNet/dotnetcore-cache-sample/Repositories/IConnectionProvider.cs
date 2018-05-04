using System.Data;

namespace Samples.CacheSample.Repositories {
    public interface IConnectionProvider
    {
        IDbConnection OpenConnection();
    }
}