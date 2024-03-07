namespace WaiterChefBoss.Contracts.Boss
{
    public interface IBossService
    {
        Task AddWaiter(int waiterId, string waiterName);

        Task AddChef(int chefId, string chefName);

        Task RemoveWaiter(int waiterId);

        Task RemoveChef(int chefId);

        Task DailyReport(DateTime today);

        Task CustomReport(DateTime start, int howManyDays);

        Task ChefReport(int chefId);

        Task WaiterReport(int waiterId);

        Task FullReport();

        Task ActiveOrders();

        Task OutOfStock();

        Task AllProducts();

        Task RemoveProduct(int productId);

        Task UpdateProduct(int productId);

        Task AddProduct(int categoryId, int productId);

        Task AddCategory(int categoryId, int productId);

        Task RemoveCategory(int categoryId);

        Task UpdateCategory(int categoryId);

    }
}
