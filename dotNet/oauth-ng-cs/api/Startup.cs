using IdentityServer3.AccessTokenValidation;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var authUrl = "http://localhost:51901";
            var appUrl = "http://localhost:51900";

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = authUrl,
                ClientId = "spa",
                ClientSecret = "59D8076E-A7B4-4348-9B02-F358A869E3B8",

                RequiredScopes = new[] { "api" }
            });

            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { Id = RouteParameter.Optional });
            config.EnableCors(new EnableCorsAttribute(appUrl, "*", "*"));
            config.Filters.Add(new AuthorizeAttribute());

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            app.UseWebApi(config);
        }
    }
}