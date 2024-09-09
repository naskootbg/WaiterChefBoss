using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Services
{
    public class EditAddService : IEditAddService
    {
        private readonly ApplicationDbContext context;
        private readonly ICategoryService category;
        private readonly IMemoryCache cache;
        private readonly IProductService productService;
        public EditAddService(ApplicationDbContext _context, ICategoryService category, IMemoryCache _cache, IProductService _productService)
        {
            context = _context;
            this.category = category;
            cache = _cache;
            productService = _productService;
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


            return id;
        }

        public async Task<int> AddProduct(ProductFormViewModel product)
        {



            var entity = new Data.Models.Product()
            {
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Status = 1,
                Calories = product.Calories,
                TimeCooking = product.TimeCooking,
                Weight = product.Weight,
                Price = product.Price,


            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            int id = entity.Id;

            var catProd = new Data.Models.CategoriesProducts
            {
                CategoryId = product.CategoryId,
                ProductId = id
            };
            await context.AddAsync(catProd);
            await context.SaveChangesAsync();

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
            // cache.Remove(DataConstants.ProductMemoryCacheKey);
            //   cache.Remove(DataConstants.CategoryMemoryCacheKey);
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            if (product != null)
            {
                context.Products.Remove(product);

                await context.SaveChangesAsync();
            }
            // cache.Remove(DataConstants.ProductMemoryCacheKey);

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

        }



        public async Task<ProductFormViewModel> EditProduct(ProductFormViewModel? product, int productId)
        {
            var ep = await context.CategoriesProducts.Where(p => p.ProductId == productId)
                .Include(p => p.Product).
                FirstOrDefaultAsync();
            if (ep != null)
            {
                if (product == null)
                {
                    var editProduct = new ProductFormViewModel
                    {
                        Id = ep.Product.Id,
                        Name = ep.Product.Name,
                        Description = ep.Product.Description,
                        ImageUrl = ep.Product.ImageUrl,
                        Status = ep.Product.Status,
                        Calories = ep.Product.Calories,
                        TimeCooking = ep.Product.TimeCooking,
                        Weight = ep.Product.Weight,
                        Price = ep.Product.Price,
                        Categories = await category.AllActiveCategories(),
                        CategoryId = ep.CategoryId

                    };
                    return editProduct;
                }
                else
                {
                    ep.Product.Id = product.Id;
                    ep.Product.Name = product.Name;
                    ep.Product.Description = product.Description;
                    ep.Product.ImageUrl = product.ImageUrl;
                    ep.Product.Status = product.Status;
                    ep.Product.Calories = product.Calories;
                    ep.Product.TimeCooking = product.TimeCooking;
                    ep.Product.Weight = product.Weight;
                    ep.Product.Price = product.Price;
                   // ep.CategoryId = product.CategoryId;

                    await context.SaveChangesAsync();

                    if (ep.CategoryId != product.CategoryId)
                    {
                        context.CategoriesProducts.Remove(ep);
                        var newProdCat = new CategoriesProducts
                        {
                            CategoryId = product.CategoryId,
                            ProductId = product.Id
                        };
                        await context.AddAsync(newProdCat);
                        await context.SaveChangesAsync();

                    }

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
                        Categories = await category.AllActiveCategories()
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
