using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaiterChefBoss.Data.Models
{
    public class CookingProducts
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DataConstants.MaxDescriptionLenght, MinimumLength = DataConstants.MinDescriptionLenght)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
    }
}
