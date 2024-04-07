using Microsoft.AspNetCore.Identity;
using WaiterChefBoss.Data.Models;

namespace WaiterChefBoss.Models
{
    public class AdminViewModel
    {
        public List<IdentityUser> Users = new List<IdentityUser>();    
        public IEnumerable<OrderFormViewModel> Orders { get; init; }  = new List<OrderFormViewModel>(); 


        

    }
}
