using System.ComponentModel.DataAnnotations;
using static WaiterChefBoss.Data.DataConstants;



namespace WaiterChefBoss.Models
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MaxTitleLenght, MinimumLength = MinTitleLenght)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(MaxDescriptionLenght, MinimumLength = MinDescriptionLenght)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0, 90)]
        public int TimeCooking { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        [Range(0.01, 200)]
        public double Price { get; set; }
        [Required]
        [Range(0, 3)]
        public int Status { get; set; }
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        [StringLength(150, MinimumLength = 15)]

        public string Calories { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }

        public List<CategoryViewModelService> Categories { get; set; } = new List<CategoryViewModelService>();
    }
}
