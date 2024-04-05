using Microsoft.EntityFrameworkCore;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

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
            var model = new ProductViewService();
            var p = await context.Products.FindAsync(id);
            if (p != null)
            {

                model =  new ProductViewService()
                {
                    Id = id,
                    Calories = p.Calories,
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
            return model;
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
                    CategoryId = categoryId,
                    
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

        public async Task<bool> ProductExists(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            return true;
        }

        public async Task<int> BlankOrder(string userId)
        {
            var order = new Order()
            {
                UserId = userId,
                Status = 0
            };
            await context.AddAsync(order);
            await context.SaveChangesAsync();
            return order.Id;

        }

        public async Task<bool> IsBlankOrder(string userId)
        {
            if (await context.Orders.FirstOrDefaultAsync(o => o.User.Id == userId && o.Status == 0) != null )
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
                Status = 1
                
            };
           
            await context.AddAsync(orderProduct);
            await context.SaveChangesAsync();       
        }

        public async Task<IEnumerable<ProductViewService>> ProductsInTheOrder(string userId)
        { 
            var model = await context
                .OrdersProducts
                .AsNoTracking()
                .Include(x => x.Product)
                .Where(o => o.UserId == userId && o.Status == 1)
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
                    OrderId = p.OrderId

                })                
                .ToListAsync();
             

            return model;
        }
       
    }
}