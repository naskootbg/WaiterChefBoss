using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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

        
        public async Task<IActionResult> Index([FromQuery] int page = 1
            , [FromQuery] int pageSize = DataConstants.NumberProductsHomePage
            , [FromQuery] string filter = "")
        {
            
             
                var products = await product.AllProducts();
                var totalCount = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (!string.IsNullOrEmpty(filter))
                {
                    products = products.Where(p => p.Name.Contains(filter) || p.Description.Contains(filter));
                }
                

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
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var products = await product.ProductSearch();
            if (!string.IsNullOrEmpty(query))
            {
                products = products.Where(p => p.Name.Contains(query) || p.Description.Contains(query));
            }

            var model = new SearchViewModel()
            {
                Products = products.ToList(),
                Search = query
            };
            return View(model);
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