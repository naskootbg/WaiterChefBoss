using System.Xml.Linq;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Services
{
    public class EditService : IEditService
    {
        private readonly ApplicationDbContext context;
        private readonly ICategoryService category;

        public EditService(ApplicationDbContext _context, ICategoryService category)
        {
            context = _context;
            this.category = category;   
        }
        public async Task AddCategory(CategoryViewModelService category)
        {
            var entity = new CategoryViewModelService
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddProduct(ProductFormViewModel product)
        {
            product.Categories = await category.AllCategories();
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
                Price = product.Price

            };
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int categoryId)
        {
            var category = await context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                context.Categories.Remove(category);

                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            if (product != null)
            {
                context.Products.Remove(product);

                await context.SaveChangesAsync();
            }
        }

        public async Task<CategoryViewModelService> EditCategory(CategoryViewModelService category, int categoryId)
        {
            var entity = await context.Categories.FindAsync(categoryId);
            if (category == null)
            {

            }
            var model = new CategoryViewModelService();
            return model;
        }

       

        public async Task<ProductFormViewModel> EditProduct(ProductFormViewModel? product, int productId)
        {
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
                        Price = ep.Price

                    };
                    entity.Categories = await category.AllCategories();
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
                        Price = product.Price

                    };
                    entity.Categories = await category.AllCategories();
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
