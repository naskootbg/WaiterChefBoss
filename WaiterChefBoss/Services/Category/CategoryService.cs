using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Services.Category
{
    public class CategoryService : ICategoryService
    {
        public readonly ApplicationDbContext context;
        private readonly IMemoryCache cache;

        public CategoryService(ApplicationDbContext _context, IMemoryCache _cache)
        {
            context = _context;
            cache = _cache;
        }

        public async Task<List<CategoryViewModelService>> AllActiveCategories()
        {
            var categories = cache.Get<List<CategoryViewModelService>>(DataConstants.CategoryMemoryCacheKey);
            if (categories == null)
            {
                categories= await context
                .Categories
                .Where(c => c.Status == 1 || c.Status == 3)
                .Select(c => new CategoryViewModelService
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();
            }
            var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(365));

            cache.Set(DataConstants.ProductMemoryCacheKey, categories, cacheOptions);
            return categories;
        }
        public async Task<List<CategoryViewModelService>> BarmanCategories()
        {
            return await context
                .Categories
                .Where(c => c.Status == 2)
                .Select(c => new CategoryViewModelService
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();
        }
        public async Task<List<CategoryViewModelService>> AllCategories()
        {
            return await context
                .Categories
                .Select(c => new CategoryViewModelService
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();
        }
        public async Task<CategoryViewModelService> CategoryDetails(int id)
        {
            var model = new CategoryViewModelService();
            var c = await context.Categories.FindAsync(id);
            
            if (c != null)
            {
                model = new CategoryViewModelService
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                };
            }


            return model;
        }

        public async Task<bool> CategoryExists(int id)
        {
             
            return await context
                .Categories.AnyAsync(c => c.Id == id);
        }
    }
}
