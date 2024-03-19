using Microsoft.EntityFrameworkCore;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Services.Category;

namespace WaiterChefBoss.Services.Product
{
    public class ProductService : IProductService
    {
        public readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext _context)
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