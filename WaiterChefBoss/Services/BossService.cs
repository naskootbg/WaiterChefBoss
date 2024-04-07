using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Win32;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;
using static WaiterChefBoss.Data.DataConstants;
namespace WaiterChefBoss.Services
{
    public class BossService : IBossService
    {
        public readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> usermanager;

        public BossService(ApplicationDbContext _context, RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _usermanager)
        {
            roleManager = _roleManager;
            context = _context;
            usermanager = _usermanager;
        }


        public async Task<string> AddToRole(string userId, string roleName)
        {
            var user = await usermanager.FindByIdAsync(userId);
            if (roleName == ChefRole || roleName == WaiterRole || roleName == BossRole)
            {
                if (await roleManager.RoleExistsAsync(roleName) == false)
                {
                    var role = new IdentityRole()
                    {
                        Name = roleName
                    };

                    await roleManager.CreateAsync(role);
                    return $"Role {roleName} added successful!";
                }

            }
            else
            {
                return $"Role {roleName} must be {ChefRole},{WaiterRole} or {BossRole} and can't be created";
            }



            if (user != null)
            {
                if (await usermanager.IsInRoleAsync(user,roleName) == false)
                {
                    await usermanager.AddToRoleAsync(user, roleName);
                    return $"The user {user.UserName} is now {roleName}!";
                }
                else
                {
                    return $"The user {user.UserName} is already {roleName}!";

                }
            }
            else
            {
                return $"User with {userId} do not exists!";
            }
           
        }

            
        

        public Task AllProducts()
        {
            throw new NotImplementedException();
        }



        public Task ChefReport(int chefId)
        {
            throw new NotImplementedException();
        }

        public Task CustomReport(DateTime start, int howManyDays)
        {
            throw new NotImplementedException();
        }

        public Task FullReport()
        {
            throw new NotImplementedException();
        }

        public Task OutOfStock()
        {
            throw new NotImplementedException();
        }

        public async Task<string> RemoveFromRole(string userId, string roleName)
        {
            var user = await usermanager.FindByIdAsync(userId);
            string[] roles = {ChefRole, WaiterRole, BossRole };

            if (user == null)
            {
                return $"User with ID:{userId} do not exists!";
               
            }
            else
            {
                if (await usermanager.IsInRoleAsync(user, roleName) == false)
                {
                    await usermanager.RemoveFromRolesAsync(user, roles);
                    return $"The user {user.UserName} is now removed from the roles {string.Join(", ", roles)}!";
                }
                else
                {
                    return $"The user {user.UserName} is not {roleName}!";

                }

            }
        }


        public Task WaiterReport(int waiterId)
        {
            throw new NotImplementedException();
        }
    }
}
