using Microsoft.EntityFrameworkCore;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Services.Product;

namespace WaiterChefBoss.Services.HomeProductService
{
    public class HomeProductService : IProductService
    {
        public readonly ApplicationDbContext context;

        public HomeProductService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<ProductViewService>> AllProductsPerCategory(int categoryId)
        {
            
             
            return await context
                .Products.Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .AsNoTracking()
                .Select(p => new ProductViewService
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Weight = p.Weight,
                    Calories = p.Calories,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Status = p.Status,
                    TimeCooking = p.TimeCooking,
                    CategoryName = p.Category.Name,
                    CategoryId = categoryId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductViewService>> AllProducts()
        {
            return await context
             .Products
             .Include(p => p.Category)
             .AsNoTracking()
             .Select(p => new ProductViewService
             {
                 Id = p.Id,
                 Name = p.Name,
                 Description = p.Description,
                 Weight = p.Weight,
                 Calories = p.Calories,
                 Price = p.Price,
                 ImageUrl = p.ImageUrl,
                 Status = p.Status,
                 TimeCooking = p.TimeCooking,
                 CategoryName = p.Category.Name,
                 CategoryId = p.CategoryId
             })
             .ToListAsync();
        }

        public Task<bool> ProductExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}