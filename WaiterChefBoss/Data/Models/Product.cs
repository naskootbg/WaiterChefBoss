using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaiterChefBoss.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal TimeCooking { get; set; }
        [Required]
        public decimal Weight { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        public string Calories { get; set; } = string.Empty;

        public int CategotyId { get; set; }
        [ForeignKey(nameof(CategotyId))]
        public Category Category { get; set; } = null!;
    }
}