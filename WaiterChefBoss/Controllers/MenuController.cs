using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WaiterChefBoss.Data;
using static WaiterChefBoss.Data.DataConstants;

namespace WaiterChefBoss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = BossRole)]
    public class MenuController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        public MenuController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [Route("build")]
        [HttpGet]
        public JsonResult BuildMenu()
        {
            var jsonData = context.Categories
                .Where(c => c.Status != 0)
                .Select(cat => new
                {
                    categoryName = cat.Name,
                    products = cat.Products.Select(p => new
                    {
                        title = p.Product.Name,
                        image = p.Product.ImageUrl,
                        description = p.Product.Description,
                        id = p.CategoryId
                    }).ToArray()
                }).ToArray();


            return new JsonResult(jsonData);
        }
    }
}
