namespace WaiterChefBoss.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ProductViewService> Products { get; init; } = new List<ProductViewService>();
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
}
}
