using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public HomeController(IProductService _product)
        {

            product = _product;
        }

        
        public async Task<IActionResult> Index()
        {

            var newModel = new HomeViewModel
            {

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