using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace Auth.Configuration
{
    public class Scopes
    {
        public static List<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,
                new Scope
                {
                    Name = "api",
                    DisplayName = "Access to API",
                    Description = "This will grant you access to the API",
                    ScopeSecrets = new List<Secret>
                    {
                        new Secret("59D8076E-A7B4-4348-9B02-F358A869E3B8".Sha256())
                    },
                    Type = ScopeType.Resource,
                    IncludeAllClaimsForUser = true
                }
            };
        }
    }
}