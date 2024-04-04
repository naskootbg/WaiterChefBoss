﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Controllers
{
    public class UserPanelController : Controller
    {
        
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> usermanager;
        private readonly ICategoryService category;
        private readonly IOrderService order;
        

        public UserPanelController(IOrderService _order, RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _usermanager, ICategoryService _category)
        {
            roleManager = _roleManager;
            order = _order;
            usermanager = _usermanager;
            category = _category;
        }
        public IEnumerable<CategoryViewModelService> Categories { get; set; } = null!;
        [Authorize]
        public async Task<IActionResult> Index()
        {
           // int status = 0;
            var user = await usermanager.FindByIdAsync(UserId());
            var roles = await usermanager.GetRolesAsync(user);
            if (roles.Contains(DataConstants.BossRole))
            {

            }
            var newUserViewModel = new UserViewModel
            {
                // Orders = await order.OrdersByStatus(status),
                 Name = user.UserName

            };
            return View(newUserViewModel);
             
        }
        public async Task<IActionResult> DispayOrders(int status)
        {
            var orders = await order.OrdersByStatus(status);
             
            return View(orders);

        }
        private string UserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}