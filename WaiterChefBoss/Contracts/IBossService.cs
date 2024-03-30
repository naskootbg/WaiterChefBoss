using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts
{
    public interface IBossService
    {
        /// <summary>
        /// When IdentityUsers.Count == 0 //First user will become a Boss - super admin
        /// </summary>

        Task BecomeBoss();

        Task<string> RemoveFromRole(string userId, string roleName);
        Task<string> AddToRole(string userId, string roleName);
        Task CustomReport(DateTime start, int howManyDays);

        Task ChefReport(int chefId);

        Task WaiterReport(int waiterId);

        Task FullReport();

        Task OutOfStock();

        Task AllProducts();

        Task RemoveProduct(int productId);

        Task UpdateProduct(ProductViewService product, int productId);

        Task AddProduct(ProductViewService product);

        Task AddCategory(CategoryViewModelService category);

        Task RemoveCategory(int categoryId);

        Task UpdateCategory(CategoryViewModelService category, int categoryId);

        //Task Reviews();

    }
}
