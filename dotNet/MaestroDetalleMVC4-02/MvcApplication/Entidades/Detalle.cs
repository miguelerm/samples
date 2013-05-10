using System.ComponentModel.DataAnnotations;

namespace MvcApplication.Entidades
{
    public class Detalle
    {
        public int FacturaId { get; set; }

        public int ProductoId { get; set; }

        [Range(0, 999999999)]
        public decimal Precio { get; set; }

        [Range(0, 999999999)]
        public decimal Cantidad { get; set; }
    }
}