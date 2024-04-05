using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;
using static WaiterChefBoss.Data.TempMessages;

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
            TempData["message"] = TempSendOrderToWaiter();

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {            
            await product.AddToCart(UserId(), productId);
            TempData["message"] = TempAddToCart(await product.ProductName(productId));
            return RedirectToAction(nameof(Cart));

           // return LocalRedirect("/");
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
