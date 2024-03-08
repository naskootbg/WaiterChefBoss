using WaiterChefBoss.Contracts.ICustomerService;
using WaiterChefBoss.Data;

namespace WaiterChefBoss.Services
{
    public class CustomerService : ICustomerService
    {
        public readonly ApplicationDbContext context;

        public CustomerService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task OrderProducts(int productId, int orderId, int productsQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
