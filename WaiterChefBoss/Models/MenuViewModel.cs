namespace WaiterChefBoss.Models
{
    public class MenuViewModel
    {
        public int CartProductsCount { get; set; }
        public int CategoryProductsCount { get; set; }

        public List<CategoryViewModelService> AllCategories { get; set; } = new List<CategoryViewModelService>();
}
}
