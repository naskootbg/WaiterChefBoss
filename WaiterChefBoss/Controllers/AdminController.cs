using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Models;
using WaiterChefBoss.Services;
using static WaiterChefBoss.Data.DataConstants;

namespace WaiterChefBoss.Controllers
{
    public class AdminController : Controller
    {
        
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> usermanager;
        private readonly ICategoryService category;
        private readonly IOrderService order;
        private readonly IBossService admin;

        public AdminController(IOrderService _order,
             RoleManager<IdentityRole> _roleManager,
             UserManager<IdentityUser> _usermanager,
             ICategoryService _category,
             IBossService _admin)
        {
            roleManager = _roleManager;
            order = _order;
            usermanager = _usermanager;
            category = _category;
            admin = _admin; 
        }


        [Authorize(Roles = Data.DataConstants.BossRole)]
        public async Task<IActionResult> Index()
        {
            // int status = 0;
            //  var user = await usermanager.FindByIdAsync(UserId());
            var model = new AdminViewModel()
            {
                Users =  await usermanager.Users.ToListAsync()
            };
            return View(model);

         
 
        }
        public async Task<IActionResult> DispayOrders(int status)
        {
            var orders = await order.OrdersByStatus(status);
             
            return View(orders);

        }
        public async Task<IActionResult> AddToRole(string Id, string roleName)
        {
            return View(await admin.AddToRole(Id, roleName));
        }
        public async Task<IActionResult> RemoveRoles(string Id, string roleName)
        {
            return View(await admin.RemoveFromRole(Id, roleName));
        }

    }
}
