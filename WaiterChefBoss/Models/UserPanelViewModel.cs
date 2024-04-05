using Microsoft.AspNetCore.Identity;
using WaiterChefBoss.Data.Models;

namespace WaiterChefBoss.Models
{
    public class UserPanelViewModel
    {
        public List<IdentityUser> Users = new List<IdentityUser>();    
        public IEnumerable<Order> Orders { get; init; }  = new List<Order>(); 


        

    }
}
