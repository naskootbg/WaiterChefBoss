namespace WaiterChefBoss.Models
{
    public class WorkerViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public IEnumerable<OrderFormViewModel> Orders { get; init; } = new List<OrderFormViewModel>();
    }
}
