using Microsoft.Extensions.Caching.Memory;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Services
{
    public class EditAddService : IEditAddService
    {
        private readonly ApplicationDbContext context;
        private readonly ICategoryService category;
        private readonly IMemoryCache cache;

        public EditAddService(ApplicationDbContext _context, ICategoryService category, IMemoryCache _cache)
        {
            context = _context;
            this.category = category;  
            cache = _cache;
        }
        public async Task<int> AddCategory(CategoryViewModelService category)
        {
            var entity = new Data.Models.Category()
            {
                
                Name = category.Name,
                Description = category.Description,
                Status = 1
            };
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            int id = entity.Id;
            cache.Remove(DataConstants.ProductMemoryCacheKey);
            cache.Remove(DataConstants.CategoryMemoryCacheKey);

            return id;
        }

        public async Task<int> AddProduct(ProductFormViewModel product)
        {
            var entity = new Data.Models.Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Status = product.Status,
                Calories = product.Calories,
                CategoryId = product.CategoryId,
                TimeCooking = product.TimeCooking,
                Weight = product.Weight,
                Price = product.Price

            };
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            int id = entity.Id;
            cache.Remove(DataConstants.ProductMemoryCacheKey);
            return id;
        }

        public async Task DeleteCategory(int categoryId)
        {
            var category = await context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                context.Categories.Remove(category);

                await context.SaveChangesAsync();
            }
            cache.Remove(DataConstants.ProductMemoryCacheKey);
            cache.Remove(DataConstants.CategoryMemoryCacheKey);
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            if (product != null)
            {
                context.Products.Remove(product);

                await context.SaveChangesAsync();
            }
            cache.Remove(DataConstants.ProductMemoryCacheKey);

        }

        public async Task<CategoryViewModelService> EditCategory(CategoryViewModelService? category, int categoryId)
        {
            var entity = await context.Categories.FindAsync(categoryId);
            if (entity != null)
            {
                if (category == null)
                {
                    var model = new CategoryViewModelService
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description,
                        Status = entity.Status
                    };
                    return model;
                }
                else
                {
                    entity.Id = category.Id;
                    entity.Name = category.Name;
                    entity.Description = category.Description;
                    entity.Status = category.Status;
                    await context.SaveChangesAsync();
                    var cat = new CategoryViewModelService
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        Status = category.Status
                    };
                    return cat;
                }
            
                
                
            }
            else
            {
                return new CategoryViewModelService { Name = "No Such Category" };
            }
            cache.Remove(DataConstants.CategoryMemoryCacheKey);

        }



        public async Task<ProductFormViewModel> EditProduct(ProductFormViewModel? product, int productId)
        {
            cache.Remove(DataConstants.ProductMemoryCacheKey);
            var ep = await context.Products.FindAsync(productId);
            if (ep != null)
            {
                if (product == null)
                {
                    var entity = new ProductFormViewModel
                    {
                        Id = ep.Id,
                        Name = ep.Name,
                        Description = ep.Description,
                        ImageUrl = ep.ImageUrl,
                        Status = ep.Status,
                        Calories = ep.Calories,
                        CategoryId = ep.CategoryId,
                        TimeCooking = ep.TimeCooking,
                        Weight = ep.Weight,
                        Price = ep.Price,
                        Categories = await category.AllCategories()
                    };
                    return entity;
                }
                else
                {
                    ep.Id = product.Id;
                    ep.Name = product.Name;
                    ep.Description = product.Description;
                    ep.ImageUrl = product.ImageUrl;
                    ep.Status = product.Status;
                    ep.Calories = product.Calories;
                    ep.CategoryId = product.CategoryId;
                    ep.TimeCooking = product.TimeCooking;
                    ep.Weight = product.Weight;
                    ep.Price = product.Price;
                    await context.SaveChangesAsync();
                    var entity = new ProductFormViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageUrl = product.ImageUrl,
                        Status = product.Status,
                        Calories = product.Calories,
                        CategoryId = product.CategoryId,
                        TimeCooking = product.TimeCooking,
                        Weight = product.Weight,
                        Price = product.Price,
                        Categories = await category.AllCategories()
                    };
                    return entity;
                }
            }
            else
            {
                return new ProductFormViewModel { Name = "No Such Product" };
            }

            

        }
    }
}
