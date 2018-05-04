using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Samples.CacheSample.Repositories {
    public class DbSchema {
        private readonly IConnectionProvider connectionProvider;
        private readonly ILogger<DbSchema> logger;

        public DbSchema(IConnectionProvider connectionProvider, ILogger<DbSchema> logger)
        {
            this.connectionProvider = connectionProvider;
            this.logger = logger;
        }

        public void Crear() {
            logger.LogDebug("Creating Database Schema...");
            using(var cnn = connectionProvider.OpenConnection())
            {
                var command = cnn.CreateCommand();
                command.CommandText = GetDatabaseSchemaScript();
                command.ExecuteNonQuery();
            }
            logger.LogDebug("Database Schema Created.");
        }

        private static string GetDatabaseSchemaScript() {
            const string resourceName = "dotnetcore-cache-sample.Resources.database-schema.sql";
            var resourceAssembly = typeof(DbSchema).Assembly;
            var resourceStream = resourceAssembly.GetManifestResourceStream(resourceName);

            if (resourceStream == null) {
                var resourceNames = string.Join(", ", resourceAssembly.GetManifestResourceNames());
                throw new ApplicationException($"Resource {resourceName} do not exists on the assembly {resourceAssembly.FullName}. Available resouces: {resourceNames}.");
            }

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}