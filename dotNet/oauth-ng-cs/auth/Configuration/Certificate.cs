using System.Security.Cryptography.X509Certificates;
using System.Web.Hosting;

namespace Auth.Configuration
{
    public class Certificate
    {
        public static X509Certificate2 Load()
        {
            var certPath = HostingEnvironment.MapPath("~/App_Data/idsrv3test.pfx");
            return new X509Certificate2(certPath, "idsrv3test");
        }
    }
}