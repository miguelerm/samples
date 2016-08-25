using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entidades
{
    [Table("FacturaDetalles")]
    public class FacturaDetalle
    {
        public int Id { get; set; }

        public int FacturaId { get; set; }

        [ForeignKey("FacturaId")]
        public Factura Factura { get; set; }

        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        public decimal Cantidad { get; set; }

        public decimal Precio { get; set; }

    }
}