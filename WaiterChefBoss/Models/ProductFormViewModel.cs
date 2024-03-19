using System.ComponentModel.DataAnnotations;

namespace WaiterChefBoss.Models
{
    public class ProductFormViewModel
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
        [Required]
        public int CategotyId { get; set; }
    }
}
