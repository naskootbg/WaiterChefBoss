using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WaiterChefBoss.Data;

namespace WaiterChefBoss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var activeCategories = context.Categories
                .Where(c => c.Status != 0).Select(c => c.Id).ToArray();


            var jsonData = new
            {
                Name = "Pranaya",
                ID = activeCategories,
               // DateOfBirth = new DateTime(1988, 02, 29)
            };
            // Returning a JsonResult object with the jsonData as the content to be serialized to JSON
            return new JsonResult(jsonData);
        }
    }
}
