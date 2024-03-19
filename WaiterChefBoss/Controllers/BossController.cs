using Microsoft.AspNetCore.Mvc;
using WaiterChefBoss.Contracts.Boss;
using WaiterChefBoss.Data.Models;

namespace WaiterChefBoss.Controllers
{
    public class BossController : Controller
    {
        private readonly IBossService bossService;

        public BossController(IBossService data)
        {
            bossService = data;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateCategory()
        {
            return View(UpdateCategory());
        }
    }
}
