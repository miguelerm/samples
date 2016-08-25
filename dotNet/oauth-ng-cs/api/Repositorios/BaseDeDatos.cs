using Api.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Api.Repositorios
{
    public class BaseDeDatos : DbContext
    {
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }
        public DbSet<Producto> Productos { get; set; }

        static BaseDeDatos()
        {
            // Esto para que Entity Framework nunca intente crear la base de datos.
            // se asume que la base de datos ya existirá.
            Database.SetInitializer(new NullDatabaseInitializer<BaseDeDatos>());
        }

        public BaseDeDatos() : base("CadenaDeConexion")
        {
        }
    }
}