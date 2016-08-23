using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace Auth.Configuration
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "Single Page Application",
                    ClientId = "spa",
                    Flow = Flows.Implicit,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:51900/signin.html",
                        "http://localhost:51900/signin-renew.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:51900/signedout.html"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:51900"
                    },
                    AllowAccessToAllScopes = true,
                    RequireConsent = false
                }
            };
        }
    }
}