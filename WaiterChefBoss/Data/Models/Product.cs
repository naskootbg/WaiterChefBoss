using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WaiterChefBoss.Data.DataConstants;

namespace WaiterChefBoss.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MaxTitleLenght, MinimumLength = MinTitleLenght)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(MaxDescriptionLenght, MinimumLength = MinDescriptionLenght)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0,90)]
        public int TimeCooking { get; set; }
        [Required]
        [Range(0, 50)]
        public double Weight { get; set; }
        [Required]
        [Range(1.00, 500.00)]
        public double Price { get; set; }
        [Required]
        [Range(0, 5)]
        public int Status { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 11)]
        public string ImageUrl { get; set; } = string.Empty;
        [StringLength(20, MinimumLength = 0)]
        public string Calories { get; set; } = string.Empty;
  
    }
}