using System.ComponentModel.DataAnnotations;

namespace WaiterChefBoss.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        public IEnumerable<Product> Products { get; init; } = new List<Product>();
    }
}
