using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Component
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public MenuViewComponent(ICategoryService _categoryService, IProductService _productService)
        {
            categoryService = _categoryService;
            productService = _productService;
        }
 
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ProductViewService> prod = new List<ProductViewService>();
            if (User.Identity?.Name != null)
            {
                prod = await productService.ProductsInTheOrder(UserId()!);

            }
            var categories = await categoryService.AllCategories();
            var model = new MenuViewModel()
            {
                AllCategories = categories,
                CartProductsCount = prod.Count()
            };

            return View(model);
        }

        private string? UserId()
        { 
            if (User.Identity?.Name != null)
            {
                var id = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).First().ToString();
                return id;
            }
            return null;
            
        }
    }
}
