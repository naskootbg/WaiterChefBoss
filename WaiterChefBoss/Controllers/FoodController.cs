using Microsoft.AspNetCore.Mvc;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Models;
using WaiterChefBoss.Services.Category;

namespace WaiterChefBoss.Controllers
{
    public class FoodController : Controller
    {
        private readonly IProductService product;
        private readonly ICategoryService category;
        public FoodController(IProductService _product, ICategoryService _category)
        {
            category = _category;
            product = _product;
            
        }
        public async Task<IActionResult> Category(int id, [FromQuery] int page = 1
            , [FromQuery] int pageSize = DataConstants.NumberProductsCategoryPage, [FromQuery] string filter = "")
        {
             
            if (await category.CategoryExists(id))
            {
                var products = await product.AllProductsPerCategory(id);
                if (!string.IsNullOrEmpty(filter))
                {
                    products = products.Where(p => p.Name.Contains(filter) || p.Description.Contains(filter));
                }
                var totalCount = products.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                products = products.Skip((page - 1) * pageSize).Take(pageSize);


                return View(new CategoryViewModel
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize,
                    CategoryDetails = await category.CategoryDetails(id),
                    Products = products.ToList()


                }); 
            }
            
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Product(int id)
        {

            if (await product.ProductExists(id))
            {
                var model = await product.ProductById(id);
                return View(model); 
            }

            return BadRequest();
        }
    }
}
