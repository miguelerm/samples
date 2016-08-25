using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.Entidades
{
    [Table("Facturas")]
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        [StringLength(20)]
        public string Nit { get; set; }

        [StringLength(300)]
        public string Nombre { get; set; }

        public ICollection<FacturaDetalle> Detalles { get; set; }

        public Factura()
        {
            Fecha = DateTime.UtcNow;
        }
    }
}