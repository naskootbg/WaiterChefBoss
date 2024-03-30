namespace WaiterChefBoss.Models
{
    public class UserViewModel
    {
        int Id {get; set;}
        public string Name { get; set; } = string.Empty;
        public IEnumerable<OrderViewModel> Orders { get; init; }  = new List<OrderViewModel>(); 


        

    }
}
