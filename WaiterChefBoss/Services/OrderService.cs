using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;
using static WaiterChefBoss.Data.DataConstants;
namespace WaiterChefBoss.Services
{
    public class OrderService : IOrderService
    {
        public readonly ApplicationDbContext context;

        public OrderService(ApplicationDbContext _context)
        {
            context = _context;
        }
        /// <summary>
        /// OrderProducts status 0 => cart
        /// OrderProducts status 1 => chef
        /// OrderProducts status 2 => barman
        /// 
        /// Order status 0 => order canceled and the first temp order
        /// Order status 1 => orders for chef
        /// Order status 2 => orders for barman
        /// Order status 3 => orders for waiter
        /// Order status 4 => orders delivered
        /// Order status 5 => orders paid and completed
        /// 
        /// 
        /// 
        /// The boss can see all orders with all statuses
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>


        public async Task<IEnumerable<OrderFormViewModel>> OrdersForWorker(string roleName)
        {
            int status = 0;
            if (roleName == ChefRole)
            {
                status = 1;
            }
            else if(roleName == BarmanRole)
            {
                status = 2;
            }
            else if (roleName == WaiterRole)
            {
                status = 3;
            }
            List<OrderFormViewModel> ordersForChef = new();
            var orders = await context
                .Orders
                .AsNoTracking()
                .Where(o => o.Status == status)
                .ToListAsync();

            foreach (var order in orders)
            {
                var productModel = await context
               .OrdersProducts
               .AsNoTracking()
               .Include(x => x.Product)
               .Include(x => x.Order)
               .Where(o => o.Status == status && o.OrderId == order.Id)
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
                var model = new OrderFormViewModel
                {
                    Id = order.Id,
                    DateAdded = order.DateAdded,
                    Table = order.Table,
                    Products = productModel,
                    Status = order.Status,
                    Total = order.Total,
                    UserId = order.UserId
                };
                ordersForChef.Add(model);
            }



            return ordersForChef;
        }

        
        public async Task PlaceOrder(string userId, int table)
        {

            DateTime dateAdded = DateTime.Now;
            double totalChef = 0.00;
            double totalBarman = 0.00;
            Order model = new();

            var orderProductsChef = await context
                .OrdersProducts
                .AsNoTracking()
                .Include(x =>x.Product)
                .Where(op => op.UserId == userId && op.Status ==  0 && op.Product.Category.Status == 1)
                .ToListAsync();
            var orderProductsBarMan = await context
                .OrdersProducts
                .AsNoTracking()
                .Include(x => x.Product)
                .Where(op => op.UserId == userId && op.Status == 0 && op.Product.Category.Status == 3)
                .ToListAsync();

            foreach (var orderProduct in orderProductsBarMan)
            {
                totalBarman += orderProduct.Product.Price;
            }
            foreach (var orderProduct in orderProductsChef)
            {
                totalChef += orderProduct.Product.Price;
            }

            if (orderProductsBarMan.Count > 0)
            {
                 model = new Order
                {
                    Status = 2,
                    UserId = userId,
                    DateAdded = dateAdded,
                    Table = table,
                    Total = totalBarman,
                    
                };
                await context.AddAsync(model);

            }
            if (orderProductsChef.Count > 0)
            {
                 model = new Order
                {
                    Status = 1,
                    UserId = userId,
                    DateAdded = dateAdded,
                    Table = table,
                    Total = totalChef
                };
                await context.AddAsync(model);

            }
           

            await context.AddAsync(model);
            await context.SaveChangesAsync();
            var id = model.Id;
            await ChangeStatusOfAllOrdersProducts(userId,id);
  
        }

        public async Task ChangeStatusOfAllOrdersProducts(string userId, int orderId)
        {

            var orderProductModelBar = await context
          .OrdersProducts
          .AsNoTracking()
          .Where(o => o.UserId == userId && o.Status == 0 && o.Product.Category.Status == 3)
          .AsNoTracking()
          .ToListAsync();

            var orderProductModelChef = await context
          .OrdersProducts
          .AsNoTracking()
          .Where(o => o.UserId == userId && o.Status == 0 && o.Product.Category.Status == 1)
          .AsNoTracking()
          .ToListAsync();
            orderProductModelBar.ForEach(s => s.Status = 2);
            orderProductModelBar.ForEach(s => s.OrderId = orderId - 1);
            orderProductModelChef.ForEach(s => s.Status = 1);
            orderProductModelChef.ForEach(s => s.OrderId = orderId);
            context.UpdateRange(orderProductModelBar);
            context.UpdateRange(orderProductModelChef);
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
        public async Task BlankOrder(string userId)
        {
            var model = new Order
            {
                Status = 0,
                UserId = userId,
                DateAdded = DateTime.Now,
                Table = 1,
                Total = 0
            };

            await context.AddAsync(model);
            await context.SaveChangesAsync();

        }

        public async Task SendToWaiter(int id)
        {
            var order = await context.Orders.FindAsync(id);
            order!.Status = 3;
            context.Update(order);
            await context.SaveChangesAsync();
        }
    }
}
