using System.ComponentModel.DataAnnotations;
using WaiterChefBoss.Data.Models;

namespace WaiterChefBoss.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModelService CategoryDetails { get; set; } = null!;
        public IEnumerable<ProductViewService> Products { get; set; } = new List<ProductViewService>();
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
