namespace WaiterChefBoss.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ProductViewService> Products { get; init; } = new List<ProductViewService>();

    }
}
