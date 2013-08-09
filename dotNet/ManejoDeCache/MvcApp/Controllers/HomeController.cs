using MvcApp.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string usuario = null)
        {
            var servicio = new EmpresaServicio();
            var empresas = servicio.ObtenerPorUsuario(usuario);

            return View(empresas);
        }

    }
}
