using System.Linq;
using System.Security.Principal;

namespace Api
{
    public static class UserExtensions
    {
        private static class ClaimTypes
        {
            private const string Schema = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/";
            public const string NameIdentifier = Schema + "nameidentifier";
            public const string GivenName = Schema + "givenname";
            public const string Surname = Schema + "surname";
            public const string EmailAddress = Schema + "emailaddress";
        }

        public static string GetGivenName(this IPrincipal user)
        {
            return GetClaimFirstValue(user, ClaimTypes.GivenName);
        }

        public static string GetSurname(this IPrincipal user)
        {
            return GetClaimFirstValue(user, ClaimTypes.Surname);
        }

        public static string GetEmailAddress(this IPrincipal user)
        {
            return GetClaimFirstValue(user, ClaimTypes.EmailAddress);
        }

        public static string GetNameIdentifier(this IPrincipal user)
        {
            return GetClaimFirstValue(user, ClaimTypes.NameIdentifier);
        }

        private static string GetClaimFirstValue(IPrincipal user, string claimType)
        {
            return (user as System.Security.Claims.ClaimsPrincipal)?
                    .Claims?
                    .FirstOrDefault(x => x.Type == claimType)
                    ?.Value;
        }
    }
}