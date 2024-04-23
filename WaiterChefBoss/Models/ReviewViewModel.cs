using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WaiterChefBoss.Data.Models;
using static WaiterChefBoss.Data.DataConstants;

namespace WaiterChefBoss.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        [Range(1,5)]
        public int Stars { get; set; }
        [Required]
        [StringLength(MaxTitleLenght, MinimumLength = MinTitleLenght)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(MaxDescriptionLenght, MinimumLength = MinDescriptionLenght)]
        public string Description { get; set; } = string.Empty;
        public string UserId { get; set; } = null!;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double AverageStars { get; set; }

        public int TotalReviews { get; set; }


    }
}
