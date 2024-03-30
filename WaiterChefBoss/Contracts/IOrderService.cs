using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts
{
    public interface IOrderService
    {
        /// <summary>
        /// ---- WAITER ---
        /// Accept order (status 2) and send to the chef
        /// Cancel order (status 0)
        /// Deliver order (status 4)
        /// ---------------
        /// ---- CHEF -----
        /// Marking orders as cooked (status 3) and sending in waiter's quenue for delivery
        /// ---------------
        /// ---- CUSTOMER -
        /// Cancel order (status 0)
        /// ---------------
        /// ---- BOSS -----
        /// Order completed (status 5)
        /// ---------------
        /// </summary>
        Task<bool> ChangeOrderStatus(Order order, int status);
        Task<bool> ChangeOrderProductStatus(string userId, int status);

        /// <summary>
        /// Status 0 for the BOSS only
        /// Status 1 for placed order - for WAITER and BOSS roles
        /// Status 2 - for CHEF and BOSS roles
        /// Status 3 - for WAITER and BOSS roles
        /// Status 4 for the CUSTOMER
        /// The BOSS can spy
        /// </summary>
        Task<IEnumerable<OrderViewModel>> OrdersByStatus(int status);
        Task<IEnumerable<OrderViewModel>> OrdersForBoss();
        Task<IEnumerable<OrderViewModel>> OrdersForChef();
        Task<IEnumerable<OrderViewModel>> OrdersForWaiter();
        Task<IEnumerable<OrderViewModel>> OrdersForCustomer();
        Task PlaceOrder(string userId, int table);
        Task<OrderViewModel> FindOrderById(int orderId);
        Task<bool> OrderExists(int id);
    }
}
