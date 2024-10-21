using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaiterChefBoss.Data;
using static WaiterChefBoss.Data.DataConstants;

namespace WaiterChefBoss.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = BossRole)]
    public class ReportsController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        public ReportsController(ApplicationDbContext _context)
        {
            context = _context;
        }

        private IQueryable<Data.Models.Order> AllOrdersByDate(string period, int howMany)
        {
            switch (period)
            {
                case "d":
                    return context.Orders
                .Where(o => o.Status != 0 && o.DateAdded >= DateTime.Now.AddDays(-howMany)).AsQueryable();
                    
                case "m":
                    return context.Orders
                .Where(o => o.Status != 0 && o.DateAdded >= DateTime.Now.AddMonths(-howMany)).AsQueryable();
                   
                case "y":
                    return context.Orders
                .Where(o => o.Status != 0 && o.DateAdded >= DateTime.Now.AddYears(-howMany)).AsQueryable();
                    
                default:
                    return context.Orders
                .Where(o => o.Status != 0 && o.DateAdded >= DateTime.Now.AddMonths(-howMany)).AsQueryable();
                    
            }
            
        }

        [Route("full")]
        [HttpGet]
        public JsonResult AllTime(int howMany, string period)
        {             

            var jsonData = AllOrdersByDate( period, howMany).Select(t => new
                {
                    date = t.DateAdded.ToString(),
                    total = t.Total
                }).ToArray();


            return new JsonResult(jsonData);
        }
        [Route("product")]
        [HttpGet]
        public JsonResult Product(int id)
        {
            var jsonData = context.Orders
                .Include(op => op.OrderProducts)
                .ThenInclude(p => p.Product)
                .Where(o => o.Status != 0 && o.OrderProducts.Any(p => p.ProductId == id))
                .AsNoTracking()
                .Select(d => new
                {
                    date = d.DateAdded.ToString(),
                    count = d.OrderProducts.Where(p => p.ProductId == id).Count()
                })
                .ToArray();



            return new JsonResult(jsonData);
        }

        [Route("category")]
        [HttpGet]
        public JsonResult Category(int id)
        {
            var jsonData = context.Orders
                 .Include(op => op.OrderProducts)
                 .ThenInclude(p => p.Category)
                 .Where(o => o.Status != 0 && o.OrderProducts.Any(p => p.CategoryId == id))
                 .AsNoTracking()
                 .Select(d => new
                 {
                     date = d.DateAdded.ToString(),
                     count = d.OrderProducts.Where(p => p.CategoryId == id).Count()
                 })
                 .ToArray();



            return new JsonResult(jsonData);
        }
        [Route("table")]
        [HttpGet]
        public JsonResult Table(int id)
        {
            var data = context.Orders
                 .Where(o => o.Status != 0 && o.Table == id)
                 .AsNoTracking()
                 .ToList();

            foreach (var item in data)
            {

            }


            return new JsonResult(data);
        }


    }
}

