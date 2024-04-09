using Microsoft.AspNetCore.Identity;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Contracts
{
    public interface IBossService
    {
        /// <summary>
        /// When IdentityUsers.Count == 0 //First user will become a Boss - super admin
        /// </summary>

        Task<string> RemoveFromRole(string userName);
        Task<string> AddToRole(string userName, string roleName);
        Task CustomReport(DateTime start, int howManyDays);

        Task ChefReport(int chefId);

        Task WaiterReport(int waiterId);

        Task FullReport();

        Task OutOfStock();

        Task AllProducts();


        //Task Reviews();

    }
}
