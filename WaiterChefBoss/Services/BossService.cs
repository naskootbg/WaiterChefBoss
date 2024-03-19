using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WaiterChefBoss.Contracts.Boss;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Services
{
    public class BossService : IBossService
    {
        public readonly ApplicationDbContext context;

        public BossService(ApplicationDbContext _context)
        {
            context = _context;
        }

        //public async Task<IActionResult> AddUserRole()
        //{

        //}

        public Task AddCategory(CategoryViewModel category)
        {
            throw new NotImplementedException();
        }

        public async Task AddChef(string id)
        {

        }

        public async Task AddProduct(ProductViewModel product)
        {

        }

        public async Task AddWaiter(string id)
        {

        }

        public async Task AllProducts()
        {

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

        //public async Task<bool> UpdateCategory(CategoryViewModel category, int categoryId)
        //{
        //    var cat = await context.Categories.FindAsync(categoryId);

        //    if (cat == null)
        //    {
        //        return false;
        //    }
        //    cat.Id = category.Id;
        //    cat.Name = category.Name;
        //    cat.Description = category.Description;
        //    return true;
        //}


        public Task UpdateProduct(ProductViewModel product, int productId)
        {
            throw new NotImplementedException();
        }

        public async Task WaiterReport(int waiterId)
        {
            throw new NotImplementedException();
        }

        Task IBossService.UpdateCategory(CategoryViewModel category, int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
