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
            new Producto { Id = 1, Nombre = "Producto A", Precio = 150 },
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
            if (modelo == null)
            {
                modelo = new Factura();
            }

            if (operacion == null)
            {
                if (CrearFactura(modelo))
                {
                    return RedirectToAction("Index");
                }
            }
            else if (operacion == "agregar-detalle")
            {
                modelo.Detalle.Add(new Detalle());
            }
            else if (operacion.StartsWith("eliminar-detalle-"))
            {
                EliminarDetallePorIndice(modelo, operacion);
            }

            ViewBag.Productos = productos;
            return View(modelo);
        }

        private static void EliminarDetallePorIndice(Factura factura, string operacion)
        {
            // se asume que en el parametro 'operacion' viene el index del detalle a eliminar.
            string indexStr = operacion.Replace("eliminar-detalle-", "");
            int index = 0;

            if (int.TryParse(indexStr, out index) && index >= 0 && index < factura.Detalle.Count)
            {
                var item = factura.Detalle.ToArray()[index];
                factura.Detalle.Remove(item);
            }
        }

        private bool CrearFactura(Factura factura)
        {
            if (ModelState.IsValid)
            {
                if (factura.Detalle != null && factura.Detalle.Count > 0)
                {
                    // este id posiblemente lo asigne tu base de datos.
                    factura.Id = facturas.Count > 0 ? facturas.Max(x => x.Id) + 1 : 1;
                    facturas.Add(factura);
                    return true;
                }
                else
                {
                    ModelState.AddModelError("", "No puede guardar facturas sin detalle");
                }
            }

            return false;
        }
    }
}
