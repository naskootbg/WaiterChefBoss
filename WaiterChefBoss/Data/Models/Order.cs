using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaiterChefBoss.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int Status { get; set; }
        [Required]
        public int Table { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();
        [Required]
        public decimal Total { get; set; }
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
        
        
    }
}
