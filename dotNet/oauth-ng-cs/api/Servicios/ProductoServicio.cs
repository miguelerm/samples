using Api.Entidades;
using Api.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Servicios
{
    public class ProductoServicio
    {
        private readonly ProductosRepositorio productos;

        public ProductoServicio()
        {
            productos = new ProductosRepositorio();
        }

        public IEnumerable<Producto> ObtenerTodos(int pagina = 1, int elementosPorPagina = 20)
        {
            if (pagina < 1) throw new ArgumentOutOfRangeException("La pagina no puede ser menor a 1");
            if (elementosPorPagina < 1) throw new ArgumentOutOfRangeException("El valor indicado para los elementos por pagina es inválido");
            if (elementosPorPagina > 500) throw new ArgumentOutOfRangeException("Por motivos de rendimiento no se permite obtener mas de 500 registros en una sola petición");

            var cantidadDeProductos = productos.ContarTodos();

            var ultimaPagina = Math.Ceiling((double)cantidadDeProductos / elementosPorPagina);

            if (pagina > ultimaPagina)throw new ArgumentOutOfRangeException($"La pagina no puede ser mayor a {ultimaPagina}.");

            var skip = (pagina - 1) * elementosPorPagina;
            var take = elementosPorPagina;

            return productos.ObtenerTodos(skip, take);
        }

    }
}