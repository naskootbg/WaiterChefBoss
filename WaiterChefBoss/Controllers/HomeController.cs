using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;
using WaiterChefBoss.Services.Category;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WaiterChefBoss.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService product;

        public HomeController(IProductService _product)
        {

            product = _product;
        }

        
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int pageSize = DataConstants.NumberProductsHomePage, [FromQuery] string filter = "")
        {
            var products = await product.AllProducts();
            if (!string.IsNullOrEmpty(filter))
            {
                products = products.Where(p => p.Name.Contains(filter) || p.Description.Contains(filter));
            }
            var totalCount = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            products = products.Skip((page - 1) * pageSize).Take(pageSize);


            var newModel = new HomeViewModel
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Products = products.ToList()

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