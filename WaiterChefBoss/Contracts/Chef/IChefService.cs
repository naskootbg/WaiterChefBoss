namespace WaiterChefBoss.Contracts.Chef
{
    public interface IChefService
    {
        /// <summary>
        /// Marking orders as cooked and sending in waiter's quenue
        /// </summary>

        Task ChangeOrderStatus(int orderId);
        /// <summary>
        /// Removing/Adding receipt from/to the menu when some ingredient missing. 
        /// Also changing order status to failed and sending report to waiter
        /// </summary>
        Task ChangeProductStatus(int productId);

        Task ActiveOrders();
    }
}
