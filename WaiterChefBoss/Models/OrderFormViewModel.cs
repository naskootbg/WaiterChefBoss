using System.ComponentModel.DataAnnotations;
using WaiterChefBoss.Data.Models;

namespace WaiterChefBoss.Models
{
    public class OrderFormViewModel 
    {
        public int Id { get; set; }
        public int Status { get; set; }
        [Required]
        [Range(1, Data.DataConstants.TablesInTheRestaurant)]
        public int Table { get; set; }
        public DateTime DateAdded { get; set; }
        public List<ProductViewService> Products { get; set; } = null!;
        public double Total { get; set; }
        public string UserId { get; set; } = string.Empty;

        public Discount Discounts { get; set; } = null!;
    }
}
