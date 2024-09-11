using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Globalization;
using WaiterChefBoss.Data;

namespace WaiterChefBoss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        public ReportsController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [Route("full")]
        [HttpGet]
        public JsonResult AllTime()
        {
            var jsonData = context.Orders
                .Where(o => o.Status != 0)
                .Select(t => new
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

