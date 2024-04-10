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

        /// <summary>
        /// Status 0 for the BOSS only
        /// Status 1 for placed order - for WAITER and BOSS roles
        /// Status 2 - for CHEF and BOSS roles
        /// Status 3 - for WAITER and BOSS roles
        /// Status 4 for the CUSTOMER
        /// The BOSS can spy
        /// </summary>
        Task<IEnumerable<ProductViewService>> OrdersByStatus(int status);
        Task<IEnumerable<OrderFormViewModel>> OrdersForChef();
        Task<IEnumerable<OrderFormViewModel>> OrdersForWaiter();
        Task<IEnumerable<ProductViewService>> OrdersForCustomer();
        Task<OrderFormViewModel> PlaceOrder(string userId, int table);
        Task<OrderFormViewModel> FindOrderById(int orderId);
        Task<bool> OrderExists(int id);

        Task<List<ProductViewService>> ProductsFromOrderProductsToOrder(int orderId);
        Task ChangeStatusOfAllOrdersProducts(string userId, int orderId, int statusBefore, int statusAfter);

        Task BlankOrder(string userId);
    }
}
