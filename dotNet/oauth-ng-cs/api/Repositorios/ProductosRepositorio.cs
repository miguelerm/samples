using Api.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Repositorios
{
    public class ProductosRepositorio
    {
        private readonly BaseDeDatos db;

        public ProductosRepositorio()
        {
            db = new BaseDeDatos();
        }

        public IEnumerable<Producto> ObtenerTodos(int skip = 0, int take = 20)
        {
            return db.Productos.OrderBy(x => x.Nombre).Skip(skip).Take(take).ToList();
        }

        public int ContarTodos()
        {
            return db.Productos.Count();
        }

        public Producto BuscarPorCodigo(string codigo)
        {
            return db.Productos.FirstOrDefault(x => x.Codigo == codigo);
        }

        public Producto BuscarPorId(int id)
        {
            return db.Productos.SingleOrDefault(x => x.Id == id);
        }

        public int Crear(Producto producto)
        {
            db.Productos.Add(producto);
            db.SaveChanges();
            return producto.Id;
        }

        public bool Editar(Producto producto)
        {
            db.Entry(producto).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool Eliminar(int id)
        {
            var producto = BuscarPorId(id);
            if (producto != null)
            {
                db.Productos.Remove(producto);
                return db.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}