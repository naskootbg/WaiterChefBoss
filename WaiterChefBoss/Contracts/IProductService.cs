using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts
{
    public interface IProductService
    { 
        Task<IEnumerable<ProductViewService>> AllProducts();
        Task<IEnumerable<ProductViewService>> AllProductsPerCategory(int categoryId);
        Task<bool> ProductExists(int id);
        Task<ProductViewService> ProductById(int id);        
        Task AddToCart(string userId, int productId);
        Task<IEnumerable<ProductViewService>> ProductSearch();

        Task RemoveFromCart(string userId, int productId);     
        Task<List<ProductViewService>> ProductsInTheOrder(string userId);

        Task<string> ProductName(int id);
    }
}
