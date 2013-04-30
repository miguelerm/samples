using System;
using System.Collections.Generic;

namespace MvcApplication.Entidades
{
    public class Factura
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string Nit { get; set; }

        public string ClienteNombre { get; set; }

        public ICollection<Detalle> Detalle { get; set; }

        public Factura()
        {
            this.Detalle = new HashSet<Detalle>();
        }
    }
}