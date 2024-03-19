using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts.Boss
{
    public interface IBossService
    {
        /// <summary>
        /// When IdentityUsers.Count == 0 //First user will become a Boss - super admin
        /// </summary>
         
        Task BecomeBoss();

       

        Task RemoveWaiter(int waiterId);

        Task RemoveChef(int chefId);
        
        Task CustomReport(DateTime start, int howManyDays);

        Task ChefReport(int chefId);

        Task WaiterReport(int waiterId);

        Task FullReport();

        Task OutOfStock();

        Task AllProducts();

        Task RemoveProduct(int productId);

        Task UpdateProduct(ProductViewModel product, int productId);

        Task AddProduct(ProductViewModel product);

        Task AddCategory(CategoryViewModel category);

        Task RemoveCategory(int categoryId);

        Task UpdateCategory(CategoryViewModel category, int categoryId);

        //Task Reviews();

    }
}
