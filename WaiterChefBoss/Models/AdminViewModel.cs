using Microsoft.AspNetCore.Identity;
using WaiterChefBoss.Data.Models;

namespace WaiterChefBoss.Models
{
    public class AdminViewModel
    {
        public List<IdentityUser> Users = new List<IdentityUser>();

        public string UserName { get; set; } = string.Empty;


        public List<string> RolesNames = new List<string>();
        public string RoleName { get; set; } = string.Empty;

        public IEnumerable<OrderFormViewModel> Orders { get; init; }  = new List<OrderFormViewModel>(); 


        

    }
}
