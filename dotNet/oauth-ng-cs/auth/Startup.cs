using Auth.Configuration;
using IdentityServer3.Core.Configuration;
using Owin;

namespace Auth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServerOptions
            {
                SiteName = "Application Signin Server",
                Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get()),
                SigningCertificate = Certificate.Load(),
                RequireSsl = false
            };

            app.UseIdentityServer(options);
        }
    }
}