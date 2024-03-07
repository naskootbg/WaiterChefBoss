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

        public DateTime DateAdded { get; set; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();

        public decimal Total { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;
        
        
    }
}
