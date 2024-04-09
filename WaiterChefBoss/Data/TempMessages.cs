namespace WaiterChefBoss.Data
{
    public class TempMessages
    {
        public static string TempAddToCart(string name) => $"You successfuly added {name} to cart!";
        public static string TempRemoveFromCart(string name) => $"You successfuly removed {name} from cart!";

        public static string TempEditProduct(string name) => $"You successfuly edited product {name}!";
        public static string TempEditCategory(string name) => $"You successfuly edited category {name}!";

        public static string TempSendOrderToWaiter() => $"Thanks for the order!";
        public static string TempSendOrderToChef() => $"Order sent for cooking!";



    }
}
