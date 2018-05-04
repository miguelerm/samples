using System.Data;
using System.Threading.Tasks;

namespace Samples.CacheSample.Repositories {
    public interface IConnectionAsyncProvider
    {
        Task<IDbConnection> OpenConnectionAsync();
    }
}