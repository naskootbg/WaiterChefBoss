using WaiterChefBoss.Contracts.All;
using WaiterChefBoss.Data;

namespace WaiterChefBoss.Services
{
    public class AllService : IAllService
    {
        public readonly ApplicationDbContext context;

        public AllService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task ActiveOrders(int status)
        {
            throw new NotImplementedException();
        }

        public async Task ChangeOrderStatus(int orderId, int status)
        {
            throw new NotImplementedException();
        }
    }
}
