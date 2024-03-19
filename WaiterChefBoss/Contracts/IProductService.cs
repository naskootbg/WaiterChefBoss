using WaiterChefBoss.Services.Product;

namespace WaiterChefBoss.Contracts
{
    public interface IProductService
    { 
        Task<IEnumerable<ProductViewService>> AllProducts();
        Task<IEnumerable<ProductViewService>> AllProductsPerCategory(int categoryId);
        Task<bool> ProductExists(int id);
    }
}
