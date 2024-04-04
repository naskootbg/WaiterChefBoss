using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Controllers
{
    public class EditController : Controller
    {
        
        private readonly IEditService editService;
        public EditController(IEditService _editService)
        {
                editService = _editService;
        }
        [Authorize]
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

            return View(model);
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

            return View(model);
        }
    }
}
