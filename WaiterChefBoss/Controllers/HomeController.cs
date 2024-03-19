using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Models;
using WaiterChefBoss.Services.Category;

namespace WaiterChefBoss.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService product;
        private readonly ApplicationDbContext data;
        private readonly ICategoryService category;
        public HomeController(IProductService _product, ApplicationDbContext _data, ICategoryService _category)
        {
            category = _category;
            product = _product;
            data = _data;
        }
        //public ActionResult _Menu()
        //{
        //    return PartialView("_MMenu", data.Categories.ToList());
        //}
        public async Task<IActionResult> Index()
        {
            var newModel = new HomeViewModel
            {
                Categories = await category.AllCategories(),
                Products = await product.AllProducts()

            };
            return View(newModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //public async Task<IEnumerable<CategoryViewModelService>> AllCategories() {
        //   return await category.AllCategories();
        //}
         

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}