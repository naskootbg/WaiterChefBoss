namespace WaiterChefBoss.Models
{
    public class WorkerViewModel
    { 
        public IEnumerable<OrderFormViewModel> OrdersChef { get; init; } = new List<OrderFormViewModel>();


        public IEnumerable<OrderFormViewModel> OrdersBar { get; init; } = new List<OrderFormViewModel>();


        public IEnumerable<OrderFormViewModel> OrdersWaiter { get; init; } = new List<OrderFormViewModel>();
    }
}
