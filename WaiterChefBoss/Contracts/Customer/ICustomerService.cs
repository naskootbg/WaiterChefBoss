namespace WaiterChefBoss.Contracts.ICustomerService
{
    public interface ICustomerService
    {
    /// <summary>
    /// Shopping List will display all products for the current order.
    /// Will add each product with new request to the database.
    /// If I have enough time will add shopping list using JavaScript/Ajax to send single request to server.
    /// </summary>

        Task OrderProducts(int productId,int orderId, int productsQuantity);
        
         
        //Task WriteProductReview();

        //Task WriteChefReview();

        //Task WriteWaiterReview();

        //Task RemoveReviews();

        //Task MyReviews();
    }
}
