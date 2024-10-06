using System.ComponentModel.DataAnnotations;

namespace Ecart.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string ProductNumber { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
