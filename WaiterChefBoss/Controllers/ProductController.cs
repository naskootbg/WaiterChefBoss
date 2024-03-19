using Microsoft.AspNetCore.Mvc;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;

namespace WaiterChefBoss.Controllers
{
    public class ProductController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
