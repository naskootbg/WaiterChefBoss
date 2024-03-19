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
        public FoodController(IProductService _product,ApplicationDbContext _data, ICategoryService _category)
        {
            category = _category;
            product = _product;
            
        }
        public async Task<IActionResult> Category(int id)
        {
             
            if (await category.CategoryExists(id))
            {
                return View(new CategoryViewModel
                {
                    Categories = await category.AllCategories(),
                    CategoryDetails = await category.CategoryDetails(id),
                    Products = await product.AllProductsPerCategory(id)


                }); ;
            }
            
            return RedirectToAction("Home");
        }
        //public async Task<IEnumerable<CategoryViewModelService>> AllCategories()
        //{
        //    return await category.AllCategories();
        //}
    }
}
