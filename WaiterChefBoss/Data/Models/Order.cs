using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int Status { get; set; }
        [Required]
        [Range(1, Data.DataConstants.TablesInTheRestaurant)]
        public int Table { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }     
        
        [Required]
        public double Total { get; set; }
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        public IEnumerable<OrderProducts> Products { get; set; } = new List<OrderProducts>();
    }
}
