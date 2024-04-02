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
        public async Task<IEnumerable<OrderViewModel>> OrdersByStatus(int status)
        {

            return await context
                .Orders
                .AsNoTracking()
                .Where(o => o.Status == status)
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    Table = o.Table,
                    DateAdded = o.DateAdded,
                    Status = status,
                    Total = o.Total,
                }).ToListAsync();            

        }

        public async Task<bool> ChangeOrderStatus(Order orderId, int status)
        {
            var order = await context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return false;
            }
            var model = new OrderViewModel
            { 
                Status = status 
            };
            await context.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<OrderViewModel>> OrdersForBoss()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderViewModel>> OrdersForChef()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderViewModel>> OrdersForCustomer()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderViewModel>> OrdersForWaiter()
        {
            throw new NotImplementedException();
        }

        public async Task PlaceOrder(string userId, int table)
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
            await ChangeStatusOfAllOrdersProducts(userId,id);
        }

        public async Task ChangeStatusOfAllOrdersProducts(string userId, int orderId)
        {

            var orderProductModel = await context
          .OrdersProducts
          .AsNoTracking()
          .Where(o => o.UserId == userId && o.Status == 1)
          .ToListAsync();
            orderProductModel.ForEach(s => s.Status = 2);
            orderProductModel.ForEach(s => s.OrderId = orderId);
            context.UpdateRange(orderProductModel);
            await context.SaveChangesAsync();
             
        }


        public async Task<OrderViewModel> FindOrderById(int orderId)
        {
            OrderViewModel model = new();
            var o = await context.Orders.FirstOrDefaultAsync(or => or.Id == orderId);
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

    }
}
