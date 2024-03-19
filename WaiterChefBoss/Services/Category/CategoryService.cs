using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
namespace WaiterChefBoss.Services.Category
{
    public class CategoryService : ICategoryService
    {
        public readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext _context)
        {
            context = _context;
        }
  
        public async Task<IEnumerable<CategoryViewModelService>> AllCategories()
        {
            return await context
                .Categories
                .Select(c => new CategoryViewModelService
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                }).ToListAsync();
        }

        public async Task<CategoryViewModelService> CategoryDetails(int id)
        {
              

            var cat = context
                .Categories
                .Where(c => c.Id == id)
                .AsNoTracking()
                .Select(c => new CategoryViewModelService
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                }).FirstOrDefaultAsync();
             
            return await cat;
        }

        public async Task<bool> CategoryExists(int id)
        {
             
            return await context
                .Categories.AnyAsync(c => c.Id == id);
        }
    }
}
