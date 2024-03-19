using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaiterChefBoss.Data.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public IEnumerable<Product> Products { get; init; } = new List<Product>();
        [Required]
        public int Table { get; set; }
        
        [Required]
        public decimal Total { get; set; }
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
    }
}
