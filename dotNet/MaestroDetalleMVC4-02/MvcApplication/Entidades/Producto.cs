using System.ComponentModel.DataAnnotations;

namespace MvcApplication.Entidades
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Range(0, 999999999)]
        public decimal Precio { get; set; }
    }
}