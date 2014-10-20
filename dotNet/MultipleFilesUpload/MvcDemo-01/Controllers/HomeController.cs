using MvcDemo_01.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo_01.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            var root = Server.MapPath("~/App_Data/Archivos");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            ViewBag.Archivos = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);

            return View();
        }

        public ActionResult CargarArchivos(CargarArchivoModel[] archivos)
        {
            if (archivos == null || archivos.Length == 0)
            {
                return RedirectToAction("Index");
            }

            var root = Server.MapPath("~/App_Data/Archivos");

            foreach (var item in archivos)
            {

                if (item.Archivo == null)
                {
                    continue;
                }

                var path = Path.Combine(root, item.Tipo);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                item.Archivo.SaveAs(Path.Combine(path, Path.GetFileName(item.Archivo.FileName)));
                
            }

            return RedirectToAction("Index");
        }
    }
}