using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication.Entidades
{
    public class Factura
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [RegularExpression("[0-9]+-[0-9kK]")]
        public string Nit { get; set; }

        [Required]
        public string ClienteNombre { get; set; }

        [Required]
        public ICollection<Detalle> Detalle { get; set; }

        public Factura()
        {
            this.Detalle = new HashSet<Detalle>();
        }
    }
}