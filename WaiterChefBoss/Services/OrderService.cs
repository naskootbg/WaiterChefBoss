using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Services
{
    public class OrderService : IOrderService
    {
        public readonly ApplicationDbContext context;

        public OrderService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<IEnumerable<ProductViewService>> OrdersByStatus(int status)
        {

                            var model = await context
               .OrdersProducts
               .AsNoTracking()
               .Include(x => x.Product)
               .Where(o => o.Status == status)
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

        public async Task<bool> ChangeOrderStatus(Order order, int status)
        {

            if (order == null)
            {
                return false;
            }

            order.Status = status;             
            await context.SaveChangesAsync();
            return true;
        }
 

        public async Task<IEnumerable<ProductViewService>> OrdersForChef()
        {
            var model = await context
               .OrdersProducts
               .AsNoTracking()
               .Include(x => x.Product)
               .Where(o => o.Status == 4)
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

        public async Task<IEnumerable<ProductViewService>> OrdersForCustomer()
        {
            var model = await context
               .OrdersProducts
               .AsNoTracking()
               .Include(x => x.Product)
               .Where(o => o.Status == 2)
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

        public async Task<IEnumerable<ProductViewService>> OrdersForWaiter()
        {
                var model = await context
               .OrdersProducts
               .AsNoTracking()
               .Include(x => x.Product)
               .Where(o => o.Status == 3)
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

        public async Task<OrderFormViewModel> PlaceOrder(string userId, int table)
        {



            DateTime dateAdded = DateTime.Now;
            double total = 0.00;
            var orderProducts = await context
                .OrdersProducts
                .AsNoTracking()
                .Where(op => op.UserId == userId && op.Status ==  1)
                .Select(p => p.Product)
                .ToListAsync();
            foreach (var product in orderProducts)
            {
                total += product.Price;
            }

            var model = new Order
            {
                Status = 1,
                UserId = userId,
                DateAdded = dateAdded,
                Table = table,
                Total = total
            };

            await context.AddAsync(model);
            await context.SaveChangesAsync();
            var id = model.Id;
            await ChangeStatusOfAllOrdersProducts(userId,id,1,2);
            var returnModel = new OrderFormViewModel()
            {
                Table = model.Table
            };
            return returnModel;
        }

        public async Task ChangeStatusOfAllOrdersProducts(string userId, int orderId, int statusBefore, int statusAfter)
        {

            var orderProductModel = await context
          .OrdersProducts
          .AsNoTracking()
          .Where(o => o.UserId == userId && o.Status == statusBefore)
          .ToListAsync();
            orderProductModel.ForEach(s => s.Status = statusAfter);
            orderProductModel.ForEach(s => s.OrderId = orderId);
            context.UpdateRange(orderProductModel);
            await context.SaveChangesAsync();
             
        }


        public async Task<OrderFormViewModel> FindOrderById(int orderId)
        {
            OrderFormViewModel model = new();
            var o = await context.Orders.FindAsync(orderId);
            if (o != null)
            {

                model.Id = orderId;
                model.DateAdded = o.DateAdded;
                model.Status = o.Status;
                model.Table = o.Table;
                model.Total = o.Total;
                 
            }
             
            return model;
        }
        public async Task<bool> OrderExists(int id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<ProductViewService>> ProductsFromOrderProductsToOrder(int orderId)
        {
            var model = await context
               .OrdersProducts
               .AsNoTracking()
               .Include(x => x.Product)
               .Where(o => o.OrderId == orderId && o.Status == 2)
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

         
    }
}
