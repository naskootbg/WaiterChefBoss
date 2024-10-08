﻿using Microsoft.CodeAnalysis;
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
        //private readonly IReviewService reviewService;

        //private readonly IMemoryCache cache;
        public ProductService(ApplicationDbContext _context)
        {

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
            var p = context.CategoriesProducts
                .Include(p => p.Product)
                .Include(c => c.Category)
                .Where(p => p.ProductId == id)
                .AsNoTracking().FirstOrDefault();
            double average = 0.00;
            var reviews = await context.Reviews
               .AsNoTracking()
               .Where(r => r.ProductId == id)
               .ToListAsync();
            int total = reviews.Count;
            if (total > 0)
            {
                int sum = 0;
                foreach (var item in reviews)
                {
                    sum += item.Stars;
                }

                average = Math.Round(sum / (double)total, 2);
            }
            return new ProductViewService()
            {
                Id = id,
                Calories = p.Product.Calories,
                CategoryId = p.Category.Id,
                Description = p.Product.Description,
                CategoryName = p.Category.Name,
                ImageUrl = p.Product.ImageUrl,
                Name = p.Product.Name,
                Price = p.Product.Price,
                Status = p.Product.Status,
                TimeCooking = p.Product.TimeCooking,
                Weight = p.Product.Weight,
                AverageStars = average,
                TotalReviews = total
            };



        }
        public async Task<IEnumerable<ProductViewService>> ProductSearch()
        {

            var products = await context
           .CategoriesProducts
            .Include(p => p.Product)
            .Include(c => c.Category)
           .AsNoTracking()
           .Select(p => new ProductViewService
           {
               Id = p.Product.Id,
               Name = p.Product.Name,
               Description = p.Product.Description,
               Weight = p.Product.Weight,
               Calories = p.Product.Calories,
               Price = p.Product.Price,
               ImageUrl = p.Product.ImageUrl,
               Status = p.Product.Status,
               TimeCooking = p.Product.TimeCooking,
               CategoryName = p.Category.Name,
               CategoryId = p.Category.Id


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
                            .CategoriesProducts.Where(p => p.Category.Id == categoryId)
            .Include(p => p.Product)
            .Include(c => c.Category)
                            .AsNoTracking()
                            .Select(p => new ProductViewService
                            {
                                Id = p.Product.Id,
                                Name = p.Product.Name,
                                Description = p.Product.Description,
                                Weight = p.Product.Weight,
                                Calories = p.Product.Calories,
                                Price = p.Product.Price,
                                ImageUrl = p.Product.ImageUrl,
                                Status = p.Product.Status,
                                TimeCooking = p.Product.TimeCooking,
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
            var products = await context
           .CategoriesProducts
              .Include(p => p.Product)
              .Include(c => c.Category)
           .AsNoTracking()
           .Select(p => new ProductViewService
           {
               Id = p.Product.Id,
               Name = p.Product.Name,
               Description = p.Product.Description,
               Weight = p.Product.Weight,
               Calories = p.Product.Calories,
               Price = p.Product.Price,
               ImageUrl = p.Product.ImageUrl,
               Status = p.Product.Status,
               TimeCooking = p.Product.TimeCooking,
               CategoryName = p.Category.Name,
               CategoryId = p.Category.Id

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
            var categoryP = await context.CategoriesProducts.AsNoTracking()
                .Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            var orderProduct = new OrderProducts
            {
                ProductId = productId,
                UserId = userId,
                Status = 0,
                OrderId = 1,
                CategoryId = categoryP!.CategoryId

            };

            await context.AddAsync(orderProduct);
            await context.SaveChangesAsync();
        }

        public async Task<List<ProductViewService>> ProductsInTheOrder(string userId)
        {
            var model = await context
                .OrdersProducts
                .AsNoTracking()
                .Where(o => o.UserId == userId && o.Status == 0)
                .OrderByDescending(p => p.Product.Name)
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
                    CategoryName = p.Category.Name,
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