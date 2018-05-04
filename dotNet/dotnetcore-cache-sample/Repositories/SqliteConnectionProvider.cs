using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace Samples.CacheSample.Repositories {
    public class SqliteConnectionProvider : IConnectionProvider, IConnectionAsyncProvider
    {
        private readonly string connectionString;

        public SqliteConnectionProvider(IOptions<SqliteOptions> options)
        {
            this.connectionString = options.Value.ConnectionString;
        }
        
        public IDbConnection OpenConnection()
        {
            var conn = CreateConnection();
            conn.Open();
            return conn;
        }

        public async Task<IDbConnection> OpenConnectionAsync()
        {
            var conn = CreateConnection();
            await conn.OpenAsync();
            return conn;
        }

        private SqliteConnection CreateConnection() {
            return new SqliteConnection(connectionString);
        }
    }
}