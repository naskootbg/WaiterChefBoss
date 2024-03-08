using WaiterChefBoss.Contracts.Boss;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;

namespace WaiterChefBoss.Services
{
    public class BossService : IBossService
    {
        public readonly ApplicationDbContext context;

        public BossService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task AddChef(Chef chef)
        {
            throw new NotImplementedException();
        }

        public async Task AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task AddWaiter(Waiter waiter)
        {
            throw new NotImplementedException();
        }

        public async Task AllProducts()
        {
            throw new NotImplementedException();
        }

        public async Task BecomeBoss()
        {
            throw new NotImplementedException();
        }

        public async Task ChefReport(int chefId)
        {
            throw new NotImplementedException();
        }

        public async Task CustomReport(DateTime start, int howManyDays)
        {
            throw new NotImplementedException();
        }

        public async Task DailyReport(DateTime today)
        {
            throw new NotImplementedException();
        }

        public async Task FullReport()
        {
            throw new NotImplementedException();
        }

        public async Task OutOfStock()
        {
            throw new NotImplementedException();
        }

        public Task RemoveCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveChef(int chefId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveWaiter(int waiterId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task WaiterReport(int waiterId)
        {
            throw new NotImplementedException();
        }
    }
}
