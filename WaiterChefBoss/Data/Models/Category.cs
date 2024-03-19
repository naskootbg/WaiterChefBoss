﻿using System.ComponentModel.DataAnnotations;
using static WaiterChefBoss.Data.DataConstants;
namespace WaiterChefBoss.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MaxTitleLenght, MinimumLength = MinTitleLenght)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(MaxDescriptionLenght, MinimumLength = MinDescriptionLenght)]
        public string Description { get; set; } = string.Empty;

      //  public IEnumerable<Product> Products { get; init; } = new List<Product>();
    }
}
