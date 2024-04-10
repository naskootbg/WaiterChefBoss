using Microsoft.AspNetCore.Identity;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using static WaiterChefBoss.Data.DataConstants;
namespace WaiterChefBoss.Services
{
    public class BossService : IBossService
    {
        public readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> usermanager;

        public BossService(ApplicationDbContext _context, UserManager<IdentityUser> _usermanager)
        {
            context = _context;
            usermanager = _usermanager;
        }


        public async Task<string> AddToRole(string userName, string roleName)
        {
            var user = await usermanager.FindByNameAsync(userName);

            if (user != null)
            {
                if (await usermanager.IsInRoleAsync(user,roleName) == false)
                {
                    await usermanager.AddToRoleAsync(user, roleName);
                    return $"The user {userName} is now {roleName}!";
                }
                else
                {
                    return $"The user {userName} is already {roleName}!";

                }
            }
            else
            {
                return $"User {userName} do not exists!";
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

        public async Task<string> RemoveFromRole(string userName)
        {
            var user = await usermanager.FindByNameAsync(userName);

            if (user == null)
            {
                return $"User {user!.UserName} do not exists!";
               
            }
            else
            {
                if (await usermanager.IsInRoleAsync(user, ChefRole))
                {
                    await usermanager.RemoveFromRoleAsync(user, ChefRole);
                    return $"The user {user.UserName} is no longer {ChefRole}!";
                }
                else if (await usermanager.IsInRoleAsync(user, BossRole))
                {
                    await usermanager.RemoveFromRoleAsync(user, BossRole);
                    return $"The user {user.UserName} is no longer {BossRole}!";
                }
                else if(await usermanager.IsInRoleAsync(user, WaiterRole))
                {
                    await usermanager.RemoveFromRoleAsync(user, WaiterRole);
                    return $"The user {user.UserName} is no longer {WaiterRole}!";
                }
                else
                {
                    return $"The user {user.UserName} is not in role!";

                }

            }
        }


        public Task WaiterReport(int waiterId)
        {
            throw new NotImplementedException();
        }
    }
}
