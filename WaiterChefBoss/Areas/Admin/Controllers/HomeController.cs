using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WaiterChefBoss.Contracts;

namespace WaiterChefBoss.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public HomeController(RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _usermanager, ICategoryService _category, IBossService _admin) : base(_roleManager, _usermanager, _category, _admin)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
