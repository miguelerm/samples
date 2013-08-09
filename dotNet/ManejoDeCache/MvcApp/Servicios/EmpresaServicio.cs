using MvcApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace MvcApp.Servicios
{
    public class EmpresaServicio
    {
        private readonly ServicioWebEmpresas.ServicioWebEmpresas servicioWeb;
        private readonly ObjectCache cache;
        private const string EMPRESAS_CACHE_KEY = "EMPRESAS";

        public EmpresaServicio()
        {
            this.servicioWeb = new ServicioWebEmpresas.ServicioWebEmpresas();
            this.cache = MemoryCache.Default;
        }

        public IEnumerable<Empresa> ObtenerPorUsuario(string usuario)
        {
            if (usuario == null)
            {
                return null;
            }

            IEnumerable<Empresa> empresas = null;

            if (ExisteEnCache(usuario))
            {
                Debug.WriteLine("Se retornaran los datos del cache");
                // si existe en el cache se retornan los valores del cache.
                empresas = ObtenerDesdeCache(usuario);
            }
            else
            {
                Debug.WriteLine("Se retornaran los datos del webservice");
                // si no existe en cache, se retornan los valores del webservice.
                empresas = ObtenerDesdeWebService(usuario);
                AgregarAlCache(usuario, empresas);
            }

            return empresas;

        }

        private void AgregarAlCache(string usuario, IEnumerable<Empresa> empresas)
        {
            var cacheKey = EMPRESAS_CACHE_KEY + "_" + usuario;
            // se indica que el cache expirará en 5 minutos.
            var politica = new CacheItemPolicy();
            politica.SlidingExpiration = TimeSpan.FromMinutes(5);
            
            cache.Set(cacheKey, empresas, politica);
        }

        private bool ExisteEnCache(string usuario)
        {
            var cacheKey = EMPRESAS_CACHE_KEY + "_" + usuario;
            return cache.Contains(cacheKey);
        }

        private IEnumerable<Empresa> ObtenerDesdeCache(string usuario)
        {
            var cacheKey = EMPRESAS_CACHE_KEY + "_" + usuario;
            return (IEnumerable<Empresa>)cache.Get(cacheKey);
        }

        private IEnumerable<Empresa> ObtenerDesdeWebService(string usuario)
        {
            var empresas = new List<Empresa>();

            // se consulta el web service.
            var wsResultado = servicioWeb.ObtenerPorUsuario(usuario);

            if (wsResultado != null && wsResultado.Length > 0)
            {
                foreach (var wsEmpresa in wsResultado)
                {

                    // se contruye una nueva empresa, esto lo hago
                    // porque mi modelo empresa es distinto a la empresa
                    // que retorna el webservice en este caso.
                    empresas.Add(new Empresa
                    {
                        Id = wsEmpresa.Id,
                        Nombre = wsEmpresa.Nombre,
                        Usuarios = wsEmpresa.Usuarios
                    });

                }
            }

            return empresas;
        }
    }
}