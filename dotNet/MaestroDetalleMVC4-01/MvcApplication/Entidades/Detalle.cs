namespace MvcApplication.Entidades
{
    public class Detalle
    {
        public int FacturaId { get; set; }

        public int ProductoId { get; set; }

        public decimal Precio { get; set; }

        public decimal Cantidad { get; set; }
    }
}