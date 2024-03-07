using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaiterChefBoss.Data.Models
{
    public class Waiter
    {
        public int Id { get; set; }

        [Required]
        public int Table;
        public IEnumerable<Order> Orders { get; init; } = new List<Order>();
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
    }
}
