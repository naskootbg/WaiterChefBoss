using WaiterChefBoss.Data.Models;

namespace WaiterChefBoss.Contracts.Boss
{
    public interface IBossService
    {
        /// <summary>
        /// When IdentityUsers.Count == 0 //First user will become a Boss - super admin
        /// </summary>
         
        Task BecomeBoss();

        Task AddWaiter(Waiter waiter);

        Task AddChef(Chef chef);

        Task RemoveWaiter(int waiterId);

        Task RemoveChef(int chefId);

        Task DailyReport(DateTime today);

        Task CustomReport(DateTime start, int howManyDays);

        Task ChefReport(int chefId);

        Task WaiterReport(int waiterId);

        Task FullReport();

        Task OutOfStock();

        Task AllProducts();

        Task RemoveProduct(int productId);

        Task UpdateProduct(Product product);

        Task AddProduct(Product product);

        Task AddCategory(Category category);

        Task RemoveCategory(int categoryId);

        Task UpdateCategory(int categoryId);

        //Task Reviews();

    }
}
