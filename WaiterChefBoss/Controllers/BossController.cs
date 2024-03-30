using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data.Models;

namespace WaiterChefBoss.Controllers
{
    public class BossController : Controller
    {
        private readonly IBossService bossServis;
         

        public BossController(IBossService _bossServis)
        {
             bossServis = _bossServis;
        }
       

        public async Task<IActionResult> AddChef(string Id)
        { 
            return View(await bossServis.AddToRole(Id, "Chef"));
        }
        public async Task<IActionResult> AddBoss(string Id)
        {
            return View(await bossServis.AddToRole(Id, "Boss"));

        }
        public async Task<IActionResult> AddWaiter(string Id)
        {
            return View(await bossServis.AddToRole(Id, "Waiter"));
        }
        public async Task<IActionResult> RemoveRoles(string Id, string roleName)
        {
            return View(await bossServis.RemoveFromRole(Id, roleName));
        }
    }
}
