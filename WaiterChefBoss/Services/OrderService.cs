﻿using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public async Task<IEnumerable<OrderFormViewModel>> OrdersForWorker(string roleName, string userId = "")
        {
            List<OrderFormViewModel> ordersList = new();
            int status = 0;
            List<Order> orders = new();

            if (roleName == ChefRole)
            {
                status = 1;
                orders = await context
                .Orders
                .AsNoTracking()
                .Where(o => o.Status == status)
                .OrderBy(o => o.Table)
                .ThenBy(o => o.DateAdded)
                .ToListAsync();
            }
            else if (roleName == BarmanRole)
            {
                status = 2;
                orders = await context
                .Orders
                .AsNoTracking()
                .Where(o => o.Status == status)
                .OrderBy(o => o.Table)
                .ThenBy(o => o.DateAdded)
                .ToListAsync();
            }
            else if (roleName == WaiterRole)
            {
                status = 3;
                orders = await context
                .Orders
                .AsNoTracking()
                .Where(o => o.Status == status || o.Status == 4)
                .OrderBy(o => o.Table)
                .ThenBy(o => o.Status)
                .ThenBy(o => o.DateAdded)
                .ToListAsync();
            }
            else
            {
                if (userId.Length > 3)
                {
                    orders = await context
                    .Orders
                    .AsNoTracking()
                    .Where(o => o.Status != 0 && o.UserId == userId)
                    .OrderBy(o => o.Status)
                    .ThenByDescending(o => o.DateAdded)
                    .ToListAsync();
                }
                else
                {

                    orders = await context
                    .Orders
                    .AsNoTracking()
                    .Where(o => o.Status == 5 && o.UserId == userId)
                    .OrderBy(o => o.DateAdded)
                    .ToListAsync();
                }


            }


            foreach (var order in orders)
            {
                var productModel = await context
               .OrdersProducts
               .AsNoTracking()
               .Include(pr => pr.Product)
               .Include(x => x.Order)
               .Where(o => (o.Status == 2 || o.Status == 1) && o.OrderId == order.Id)
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
                ordersList.Add(model);
            }



            return ordersList;
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
                .Include(x => x.Product)
                .Where(op => op.UserId == userId && op.Status == 0 && op.Category.Status == 1)
                .ToListAsync();
            var orderProductsBarMan = await context
                .OrdersProducts
                .AsNoTracking()
                .Include(x => x.Product)
                .Where(op => op.UserId == userId && op.Status == 0 && op.Category.Status == 3)
                .ToListAsync();

            foreach (var orderProduct in orderProductsBarMan)
            {
                totalBarman += orderProduct.Product.Price;
            }
            foreach (var orderProduct in orderProductsChef)
            {
                totalChef += orderProduct.Product.Price;
            }
            var orderTotal = totalBarman + totalChef;
            int percent = await DiscountPercent((int)Math.Round(orderTotal));
            totalBarman = totalBarman - totalBarman * percent / 100;
            totalChef = totalChef - totalChef * percent / 100;
            if (orderProductsBarMan.Count > 0)
            {
                model = new Order
                {
                    Status = 2,
                    UserId = userId,
                    DateAdded = dateAdded,
                    Table = table,
                    Total = Math.Round(totalBarman, 2),

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
                    Total = Math.Round(totalChef, 2)
                };
                await context.AddAsync(model);

            }


            await context.AddAsync(model);
            await context.SaveChangesAsync();
            var id = model.Id;
            await ChangeStatusOfAllOrdersProducts(userId, id);

        }

        public async Task ChangeStatusOfAllOrdersProducts(string userId, int orderId)
        {

            var orderProductModelBar = await context
          .OrdersProducts
          .AsNoTracking()
          .Where(o => o.UserId == userId && o.Status == 0 && o.Category.Status == 3)
          .AsNoTracking()
          .ToListAsync();

            var orderProductModelChef = await context
          .OrdersProducts
          .AsNoTracking()
          .Where(o => o.UserId == userId && o.Status == 0 && o.Category.Status == 1)
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

        public async Task<List<OrderFormViewModel>> FindOrdersByUserId(string userId)
        {

            var orders = await context.Orders
                .AsNoTracking()
                .Where(or => or.UserId == userId && or.Status == 5)
                .Select(o => new OrderFormViewModel()
                {
                    Id = o.Id,
                    DateAdded = o.DateAdded,
                    Status = o.Status,
                    Table = o.Table,
                    Total = o.Total
                })
        .ToListAsync();




            return orders;
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
                   CategoryName = p.Category.Name,
                   OrderProductId = p.OrderId

               })
               .ToListAsync();


            return model;
        }


        public async Task SendToWaiter(int id)
        {
            var order = await context.Orders.FindAsync(id);
            order!.Status = 3;
            context.Update(order);
            await context.SaveChangesAsync();
        }
        public async Task MarkAsDelivered(int id)
        {
            var order = await context.Orders.FindAsync(id);
            order!.Status = 4;
            context.Update(order);
            await context.SaveChangesAsync();
        }
        public async Task MarkAsPaid(int id)
        {
            var order = await context.Orders.FindAsync(id);
            order!.Status = 5;
            context.Update(order);
            await context.SaveChangesAsync();
        }

        public async Task<int> DiscountPercent(int total)
        {
            var discount = await context.Discounts
                .AsNoTracking()
                .Where(d => d.Total <= total)
                .OrderByDescending(d => d.Total)
                .FirstOrDefaultAsync();


            if (discount != null)
            {
                return discount.Percent;
            }
            else
            {
                return 0;
            }
        }
    }
}
