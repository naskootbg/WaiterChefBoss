using System.ComponentModel.DataAnnotations.Schema;

namespace WaiterChefBoss.Data.Models
{
    public class CategoriesProducts
    {
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
    }
}
