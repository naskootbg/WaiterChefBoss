namespace WaiterChefBoss.Models
{
    public class SearchViewModel
    {
        public IEnumerable<ProductViewService> Products { get; set; } = new List<ProductViewService>();

        public string Search { get; set; } = string.Empty;
    }
}
