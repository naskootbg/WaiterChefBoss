using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Services.Category
{
    public class CategoryService : ICategoryService
    {
        public readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<List<CategoryViewModelService>> AllActiveCategories()
        {
            return await context
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
