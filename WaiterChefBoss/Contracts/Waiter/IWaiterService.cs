namespace WaiterChefBoss.Contracts.Waiter
{
    public interface IWaiterService
    {
        Task TakeOrder(int orderId);

        Task ActiveOrders();
    }
}
