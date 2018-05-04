using System.ComponentModel.DataAnnotations;

namespace Samples.CacheSample.Models {
    public class CreateOrUpdateBookModel {
        [Required]
        [StringLength(500)]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        [MinLength(1)]
        public string Author { get; set; }

        public int Year { get; set; }
    }
}