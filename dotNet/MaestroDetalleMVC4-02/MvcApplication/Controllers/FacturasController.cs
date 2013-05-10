using MvcApplication.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication.Controllers
{
    public class FacturasController : Controller
    {
        private static readonly ICollection<Factura> facturas = new HashSet<Factura>();

        private static readonly ICollection<Producto> productos = new HashSet<Producto>() {
            new Producto { Id = 1, Nombre = "Producto' A", Precio = 150 },
            new Producto { Id = 2, Nombre = "Producto B", Precio = 280 },
            new Producto { Id = 3, Nombre = "Producto C", Precio = 300 }
        };

        public ActionResult Index()
        {
            var modelo = facturas.ToArray();
            return View(modelo);
        }

        public ActionResult Create()
        {
            ViewBag.Productos = productos;
            return View();
        }

        public ActionResult Details(int id)
        {
            var modelo = facturas.FirstOrDefault(x => x.Id == id);

            if (modelo == null)
            {
                return HttpNotFound();
            }

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Create(Factura modelo, string operacion = null)
        {
            if (ModelState.IsValid)
            {
                if (modelo.Detalle == null || modelo.Detalle.Count == 0)
                {
                    ModelState.AddModelError("Detalle", "Debe agregar al menos un detalle para la factura");
                }
                else
                {
                    facturas.Add(modelo);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Productos = productos;
            return View(modelo);
        }
    }
}