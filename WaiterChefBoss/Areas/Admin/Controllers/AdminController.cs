using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Areas.Admin.Models;
using static WaiterChefBoss.Data.DataConstants;

namespace WaiterChefBoss.Areas.Admin.Controllers
{
    [Area(Data.Constants.AreaName)]
    [Authorize(Roles = BossRole)]
    public class AdminController : Controller
    {

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> usermanager;
        private readonly ICategoryService category;

        private readonly IBossService admin;

        public AdminController(RoleManager<IdentityRole> _roleManager,
             UserManager<IdentityUser> _usermanager,
             ICategoryService _category,
             IBossService _admin)
        {
            roleManager = _roleManager;
            usermanager = _usermanager;
            category = _category;
            admin = _admin;
        }



        public async Task<IActionResult> Index()
        {
            var model = new AdminViewModel()
            {
                Users = await usermanager.Users.ToListAsync(),
                RolesNames = await roleManager.Roles.Select(r => r.Name).ToListAsync()

            };
            return View(model);



        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(string userName, string roleName)
        {

            var user = await usermanager.FindByNameAsync(userName);
            if (user != null)
            {
                if (await usermanager.IsInRoleAsync(user, roleName) == false)
                {
                    await usermanager.AddToRoleAsync(user, roleName);
                    TempData["message"] = $"The user {userName} is now {roleName}!";
                }
                else
                {
                    TempData["message"] = $"The user {userName} is already {roleName}!";

                }
            }
            else
            {
                TempData["message"] = $"User {userName} do not exists!";
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromRoles(string userName, string roleName)
        {


            TempData["message"] = await admin.RemoveFromRole(userName);

            return RedirectToAction(nameof(Index));
        }
        public PartialViewResult Products()
        {
            return PartialView("_Products");
        }
        private string UserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
