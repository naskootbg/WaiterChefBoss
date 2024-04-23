using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Controllers
{
    [Authorize(Roles = Data.DataConstants.BossRole)]
    public class EditController : Controller
    {
        
        private readonly IEditAddService editService;
        public EditController(IEditAddService _editService)
        {
                editService = _editService;
        }
         
        [HttpGet]
        public async Task<IActionResult> Product(int id)
        {
            var model = await editService.EditProduct(null, id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Product(ProductFormViewModel product, int id)
        {
            var model = await editService.EditProduct(product, id);

            return RedirectToAction("Product", "Food", new { id });
        }
        [HttpGet]
        public async Task<IActionResult> Category(int id)
        {
            var model = await editService.EditCategory(null, id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Category(CategoryViewModelService category, int id)
        {
            var model = await editService.EditCategory(category, id);

            return RedirectToAction("Category", "Food", new { id });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
             await editService.DeleteCategory(id);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await editService.DeleteProduct(id);

            return RedirectToAction("Index", "Home");
        }
    }
}
