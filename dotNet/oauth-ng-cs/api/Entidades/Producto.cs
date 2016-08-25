using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entidades
{
    [Table("Productos")]
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(3000)]
        public string Descripcion { get; set; }

        [Range(0, 999999999)]
        public decimal Existencia { get; set; }

        [Range(0, 999999999)]
        public decimal PrecioBase { get; set; }
    }
}