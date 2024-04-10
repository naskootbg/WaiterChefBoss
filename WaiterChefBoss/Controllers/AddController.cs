using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Controllers
{
    [Authorize(Roles = Data.DataConstants.BossRole)]
    public class AddController : Controller
    {
        private readonly IEditAddService addService;
        private readonly ICategoryService category;

        public AddController(IEditAddService _addService, ICategoryService category)
        {
            addService = _addService;
            this.category = category;   
        }
        [HttpGet]
        public async Task<IActionResult> Product()
        {
            var model = new ProductFormViewModel()
            {
                Categories = await category.AllCategories()
            };
            

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Product(ProductFormViewModel product)
        {
            
            
            await addService.AddProduct(product);
            return RedirectToAction("Index", "UserPanel");
            //return RedirectToAction("Product" , "Food", new { id = id });
        }
        [HttpGet]
        public Task<IActionResult> Category()
        {           
            return Task.FromResult<IActionResult>(View());
        }
        [HttpPost]
        public async Task<IActionResult> Category(CategoryViewModelService category)
        {
            int id= await addService.AddCategory(category);
            return RedirectToAction("Category", "Food", new { id = id });

        }
    }
}
