using WaiterChefBoss.Services.Category;
using WaiterChefBoss.Services.Product;

namespace WaiterChefBoss.Models
{
    public class HomeViewModel
    {
        public IEnumerable<CategoryViewModelService> Categories { get; init; } = new List<CategoryViewModelService>();
        public IEnumerable<ProductViewService> Products { get; init; } = new List<ProductViewService>();

    }
}
