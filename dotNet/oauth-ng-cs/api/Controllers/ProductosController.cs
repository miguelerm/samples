using Api.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
    public class ProductosController : ApiController
    {
        private readonly ProductoServicio productos;

        public ProductosController()
        {
            productos = new ProductoServicio();
        }

        public IHttpActionResult Get(int pagina = 1, int elementos = 20)
        {
            return Ok(productos.ObtenerTodos(pagina, elementos));
        }
    }
}