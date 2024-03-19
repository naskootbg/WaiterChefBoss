using System.ComponentModel.DataAnnotations;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Services.Category;
using WaiterChefBoss.Services.Product;

namespace WaiterChefBoss.Models
{
    public class CategoryViewModel
    {
        public IEnumerable<CategoryViewModelService> Categories { get; init; } = new List<CategoryViewModelService>();
        public CategoryViewModelService CategoryDetails { get; set; } = null!;
        public IEnumerable<ProductViewService> Products { get; set; } = new List<ProductViewService>();
    }
}
