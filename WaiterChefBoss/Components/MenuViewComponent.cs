using Microsoft.AspNetCore.Mvc;
using System.Text;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Component
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;
        private readonly StringBuilder sb = new StringBuilder();
         
        public MenuViewComponent(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
 
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await categoryService.AllCategories();
             

            return View(categories);
        }
        
        
    }
}
