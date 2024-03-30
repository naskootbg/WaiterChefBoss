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
            await order.ChangeOrderProductStatus(userId,0);
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = UserId();
            var id = 0;
            if (await product.IsBlankOrder(userId))
            {
                id = await product.BlankOrder(userId);
            }
            else
            {
                id = await product.GetOrderId(userId);
            }
            
            await product.AddToCart(userId, productId, id);

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
