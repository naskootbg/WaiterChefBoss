using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Models;
using static WaiterChefBoss.Data.DataConstants;
using static WaiterChefBoss.Data.TempMessages;

namespace WaiterChefBoss.Controllers
{
    [Authorize]

    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public OrderController(IOrderService _orderService, IProductService _productService, ICategoryService _categoryService)
        {
            orderService = _orderService;
            productService = _productService;
            categoryService = _categoryService;
        }
        public async Task<IActionResult> View(int id)
        {
            if (await orderService.OrderExists(id))
            {
                var products = await orderService.ProductsFromOrderProductsToOrder(id);
                var orderForModel = await orderService.FindOrderById(id);
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
                await orderService.PlaceOrder(UserId(), table);
                TempData["message"] = TempSendOrderToWaiter();

                return RedirectToAction("Index", "Home");
            }

        }
        [HttpGet]
        public async Task<IActionResult> Cart()
        {

            var products = await productService.ProductsInTheOrder(UserId());
            var model = new OrderFormViewModel()
            {
                Products = products
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            await productService.AddToCart(UserId(), productId);
            TempData["message"] = TempAddToCart(await productService.ProductName(productId));
            return RedirectToAction(nameof(Cart));

            // return LocalRedirect("/");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await productService.RemoveFromCart(UserId(), productId);
            TempData["message"] = TempRemoveFromCart(await productService.ProductName(productId));
            return RedirectToAction(nameof(Cart));

            // return LocalRedirect("/");
        }
        [HttpPost]
        public async Task<IActionResult> SendToWaiter(int id, string roleName) 
        {
            await orderService.SendToWaiter(id);
            return RedirectToAction(roleName);

        }
        [Authorize(Roles = $"{BossRole}")]
        public async Task<PartialViewResult> OrdersFromAll()
        {
            var model = new WorkerViewModel()
            {
                OrdersBar = await orderService.OrdersForWorker(BarmanRole),
                OrdersChef = await orderService.OrdersForWorker(ChefRole),
                OrdersWaiter = await orderService.OrdersForWorker(BarmanRole)
            };
            return PartialView("_Orders", model);
        }
        [Authorize(Roles = $"{BossRole}, {ChefRole}")]
        public async Task<IActionResult> Chef() => View(await orderService.OrdersForWorker(ChefRole));

        [Authorize(Roles = $"{BossRole}, {WaiterRole}")]
        public async Task<IActionResult> Waiter() => View(await orderService.OrdersForWorker(WaiterRole));

        [Authorize(Roles = $"{BossRole}, {BarmanRole}")]
        public async Task<IActionResult> Barman() => View(await orderService.OrdersForWorker(BarmanRole));
        [Authorize(Roles = WaiterRole)]
        public async Task<IActionResult> MarkAsDelivered(int id) 
        {
            await orderService.MarkAsDelivered(id);
            return RedirectToAction(WaiterRole);
        }
        [Authorize(Roles = WaiterRole)]
        public async Task<IActionResult> MarkAsPaid(int id)
        {
            await orderService.MarkAsPaid(id);
            return RedirectToAction(WaiterRole);
        }
        private string UserId()
        {
            return  User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
