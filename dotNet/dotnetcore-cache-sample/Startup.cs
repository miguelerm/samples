using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Samples.CacheSample.Repositories;

namespace Samples.CacheSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<DbSchema>();
            services.AddTransient<IConnectionProvider, SqliteConnectionProvider>();
            services.AddTransient<IBooksRepository, BooksSqliteRepository>();
            services.Decorate<IBooksRepository, BooksCachedRepository>();

            services.AddLogging();
            services.AddMemoryCache();
            services.AddMvc();

            services.Configure<SqliteOptions>(options => {
                options.ConnectionString = Configuration.GetConnectionString("DefaultConnectionString");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            using(var startupScope = app.ApplicationServices.CreateScope()) {
                var schema = startupScope.ServiceProvider.GetRequiredService<DbSchema>();
                schema.Crear();
            }

        }
    }
}
