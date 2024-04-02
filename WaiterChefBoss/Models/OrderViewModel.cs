using WaiterChefBoss.Data.Models;

namespace WaiterChefBoss.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int Table { get; set; }
        public DateTime DateAdded { get; set; }
        public IEnumerable<OrderProducts> Products { get; set; } = null!;
        public double Total { get; set; }
        public string UserId { get; set; } = string.Empty;


    }
}
