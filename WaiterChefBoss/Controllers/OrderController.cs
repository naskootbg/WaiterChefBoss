using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Drawing;
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
        public async Task<IActionResult> View(int id)
        {
            if (await order.OrderExists(id))
            {
                var products = await order.ProductsFromOrderProductsToOrder(id);
                var orderForModel = await order.FindOrderById(id);
                var model = new OrderFormViewModel()
                {
                    DateAdded = orderForModel.DateAdded,
                    Products = products,
                    Table = orderForModel.Table,
                    Total = orderForModel.Total,
                    UserId = orderForModel.UserId
                };
                return View(model);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int table)
        {
            if (table < 1 || table > DataConstants.TablesInTheRestaurant)
            {
                string teststring = $"Please enter valid table number between 1 and {DataConstants.TablesInTheRestaurant}";
                TempData["message-danger"] = teststring;
                return RedirectToAction(nameof(Cart));
            }
            else
            {
                var userId = UserId();
                var placedOrder = await order.PlaceOrder(userId, table);
                TempData["message"] = TempSendOrderToWaiter();

                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Cart()
        {

            var products = await product.ProductsInTheOrder(UserId());
            var model = new OrderFormViewModel()
            {
                Products = products
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {            
            await product.AddToCart(UserId(), productId);
            TempData["message"] = TempAddToCart(await product.ProductName(productId));
            return RedirectToAction(nameof(Cart));

           // return LocalRedirect("/");
        }

       
        private string UserId()
        {
            return  User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
