using System.Data;
using System.Threading.Tasks;

namespace Samples.CacheSample.Repositories {
    public interface IConnectionProvider
    {
        IDbConnection OpenConnection();
        Task<IDbConnection> OpenConnectionAsync();
    }
}