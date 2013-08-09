using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebServices.Entidades;

namespace WebServices.ServiciosWeb
{
    /// <summary>
    /// Summary description for ServicioEmpresas
    /// </summary>
    [WebService(Namespace = "http://miguelerm.github.io/samples/serviciosweb")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ServicioWebEmpresas : System.Web.Services.WebService
    {

        private readonly IEnumerable<Empresa> empresas = new[] { 
            new Empresa { Id = 1, Nombre = "Empresa 1", Usuarios = new [] {"usuario1", "usuario2", "usuario3"} },
            new Empresa { Id = 2, Nombre = "Empresa 2", Usuarios = new [] {"usuario1"} },
            new Empresa { Id = 3, Nombre = "Empresa 3", Usuarios = new [] {"usuario2"} },
            new Empresa { Id = 4, Nombre = "Empresa 4", Usuarios = new [] {"usuario3"} },
            new Empresa { Id = 5, Nombre = "Empresa 5", Usuarios = new [] {"usuario2", "usuario3"} },
            new Empresa { Id = 6, Nombre = "Empresa 6", Usuarios = new [] {"usuario1", "usuario3"} },
            new Empresa { Id = 7, Nombre = "Empresa 7", Usuarios = new [] {"usuario1", "usuario2"} }
        };

        [WebMethod]
        public Empresa[] ObtenerPorUsuario(string usuario)
        {
            // se retornan las empresas que apliquen para el usuario indicado.
            return empresas.Where(empresa => empresa.Usuarios.Contains(usuario)).ToArray();
        }
    }
}
