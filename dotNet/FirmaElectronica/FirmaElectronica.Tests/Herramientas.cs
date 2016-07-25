using System;

namespace FirmaElectronica.Tests
{
    internal static class Herramientas
    {
        internal static string GetResourcesPath() 
        {
            return System.IO.Path.Combine (GetAssemblyDirectory (), "Recursos");
        }

        internal static string GetAssemblyDirectory() 
        {
            string codeBase = System.Reflection.Assembly.GetExecutingAssembly ().CodeBase;
            UriBuilder uri = new UriBuilder (codeBase);
            string path = Uri.UnescapeDataString (uri.Path);
            return System.IO.Path.GetDirectoryName (path);
        }
    }
}

