
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WaiterChefBoss.Data.DataConstants;

namespace WaiterChefBoss.Data.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        [Required]
        [StringLength(MaxTitleLenght, MinimumLength = MinTitleLenght)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(MaxDescriptionLenght, MinimumLength = MinDescriptionLenght)]
        public string Description { get; set; } = string.Empty;
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

    }
}
