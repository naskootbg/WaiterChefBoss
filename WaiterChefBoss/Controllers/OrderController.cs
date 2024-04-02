using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService order;
        private readonly IProductService product;
        
        public OrderController(IOrderService _order, IProductService _product ) 
        {
            order = _order;
            product = _product;
             
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int table)
        {
            var userId = UserId();
            await order.PlaceOrder(userId, table);
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {            
            await product.AddToCart(UserId(), productId);

            return RedirectToAction(nameof(Cart));             

        }

        public async Task<IActionResult> Cart()
        {
             
            var model = await product.ProductsInTheOrder(UserId());            
            return View(model);
        }
        private string UserId()
        {
            return  User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
