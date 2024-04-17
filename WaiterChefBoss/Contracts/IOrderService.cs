using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts
{
    public interface IOrderService
    {
        /// <summary>
        /// OrderProducts status 0 => cart
        /// OrderProducts status 1 => chef
        /// OrderProducts status 2 => barman
        /// 
        /// Order status 0 => order canceled and the first temp order
        /// Order status 1 => orders for chef
        /// Order status 2 => orders for barman
        /// Order status 3 => orders for waiter
        /// Order status 4 => orders delivered
        /// Order status 5 => orders paid and completed
        /// 
        /// 
        /// 
        /// The boss can see all orders with all statuses
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        
        Task<IEnumerable<OrderFormViewModel>> OrdersForWorker(string roleName);
        Task PlaceOrder(string userId, int table);
        Task<OrderFormViewModel> FindOrderById(int orderId);
        Task<bool> OrderExists(int id);

        Task SendToWaiter(int orderId);

        Task<List<ProductViewService>> ProductsFromOrderProductsToOrder(int orderId);
        Task ChangeStatusOfAllOrdersProducts(string userId, int orderId);
        Task MarkAsDelivered(int id);
        Task MarkAsPaid(int id);
        Task BlankOrder(string userId);
    }
}
