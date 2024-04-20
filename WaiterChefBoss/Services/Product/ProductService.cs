using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;
        private readonly IMemoryCache cache;
        public ProductService(ApplicationDbContext _context, IMemoryCache _cache)
        {
            cache = _cache;
            context = _context;
        }
        public async Task<string> ProductName(int id)
        {
            var name = "";
            var p = await context.Products.FindAsync(id);
            if (p != null)
            {
                name = p.Name;
            }
                return name;
        }
        public async Task<ProductViewService> ProductById(int id)
        {
            var p = await context
               .Products.Where(p => p.Id == id)
               .Include(p => p.Category)
               .AsNoTracking()
               .FirstOrDefaultAsync();

            return new ProductViewService()
                {
                    Id = id,
                    Calories = p!.Calories,
                    CategoryId = p.CategoryId,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Price = p.Price,
                    Status = p.Status,
                    TimeCooking = p.TimeCooking,
                    Weight = p.Weight
                };

            
            
        }
         public async Task<IEnumerable<ProductViewService>> ProductSearch()
        {
             
                var products = await context
               .Products
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
          
            return products.AsQueryable();
        }
        public async Task<IEnumerable<ProductViewService>> AllProductsPerCategory(int categoryId)
        {

            //var products = cache.Get<IEnumerable<ProductViewService>>(DataConstants.ProductMemoryCacheKey);
            //if (products == null)
            //{
                var products = await context
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
                                    CategoryId = categoryId,

                                })
                                .ToListAsync();
            //}
            //var cacheOptions = new MemoryCacheEntryOptions()
            //        .SetAbsoluteExpiration(TimeSpan.FromSeconds(200));

            //cache.Set(DataConstants.ProductMemoryCacheKey, products, cacheOptions);
            return products.AsQueryable();
        }

        public async Task<IEnumerable<ProductViewService>> AllProducts()
        {
            //var products = cache.Get<IEnumerable<ProductViewService>>(DataConstants.ProductMemoryCacheKey);
            //if(products == null)
            //{
              var  products = await context
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

            //}
            //var cacheOptions = new MemoryCacheEntryOptions()
            //        .SetAbsoluteExpiration(TimeSpan.FromSeconds(200));

            //cache.Set(DataConstants.ProductMemoryCacheKey, products, cacheOptions);



            return products.AsQueryable();
        }

        public async Task<bool> ProductExists(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            return true;
        }

 

        public async Task AddToCart(string userId, int productId)
        {
             
            var orderProduct = new OrderProducts
            {
                ProductId = productId,
                UserId = userId,
                Status = 0,
                OrderId = 1
                
            };
           
            await context.AddAsync(orderProduct);
            await context.SaveChangesAsync();       
        }

        public async Task<List<ProductViewService>> ProductsInTheOrder(string userId)
        { 
            var model = await context
                .OrdersProducts
                .AsNoTracking()
                .Include(x => x.Product)
                .Where(o => o.UserId == userId && o.Status == 0)
                .OrderByDescending(p=>p.Product.Name)
                .Select(p => new ProductViewService
                {
                    Id = p.Product.Id,
                    Name = p.Product.Name,
                    Description = p.Product.Description,
                    Weight = p.Product.Weight,
                    Calories = p.Product.Calories,
                    Price = p.Product.Price,
                    ImageUrl = p.Product.ImageUrl,
                    TimeCooking = p.Product.TimeCooking,
                    CategoryName = p.Product.Category.Name,
                    OrderProductId = p.OrderId

                })                
                .ToListAsync();
             

            return model;
        }

        public async Task RemoveFromCart(string userId, int productId)
        {
            var productToRemove = await context
                .OrdersProducts
                .Where(p => p.UserId == userId && p.ProductId == productId && p.Status == 0)
                .FirstOrDefaultAsync();
            if (productToRemove != null)
            {
                context.OrdersProducts.Remove(productToRemove);

                await context.SaveChangesAsync();
            }

             
        }

    }
}