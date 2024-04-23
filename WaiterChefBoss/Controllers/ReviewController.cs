using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;
using static WaiterChefBoss.Data.DataConstants;

namespace WaiterChefBoss.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        public ReviewController(IReviewService _reviewService)
        {
            reviewService = _reviewService;
        }
        [Authorize(Roles = $"{BossRole}")]
        public async Task<IActionResult> All()
        {
            var reviews = await reviewService.All();
            if (reviews == null)
            {
                return BadRequest();
            }
            return View(reviews);
        }
        
        public async Task<IActionResult> Product(int id)
        {
            var average = 0.00;
            var total = await reviewService.ProductReviewsCount(id);
            if (total != 0)
            {
                 average = await reviewService.AverageScore(id);

            }
            else
            {
                TempData["message-danger"] = "This product have not reviews. Be the first and write one";
                return RedirectToAction(nameof(Add), new { id });
            }
            var reviews = await reviewService.ProductReviews(id, average, total);
            if (reviews == null)
            {
                return BadRequest();
            }
            return View(reviews);
        }

        public async Task<IActionResult> MyReviews()
        {
            var reviews = await reviewService.MyReviews(UserId());
            if (reviews == null)
            {
                return BadRequest();
            }
            return View(reviews);
        }
        [HttpGet]
        public async Task<IActionResult> Add(int id, string name)
        {
            var model = new ReviewViewModel()
            {
                ProductId = id,
                ProductName = name
            };
            return await Task.FromResult<IActionResult>(View(model));
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(ReviewViewModel model)
        {
            int revId= await reviewService.Add(model, UserId());
            if (revId == 0)
            {
                return BadRequest();
            }
            return RedirectToAction("Show",new {id = revId});
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await reviewService.ReviewIsFromTheUser(id, UserId()) == false)
            {
                return BadRequest();
            }
            var reviews = await reviewService.Edit(id, null);
            return View(reviews);
        }
         
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReviewViewModel model)
        {
            if (await reviewService.ReviewIsFromTheUser(id,UserId()) == false)
            {
                return BadRequest();
            }
            var reviews = await reviewService.Edit(id, model);
            return RedirectToAction("Show", new {id});
        }
       
        public async Task<IActionResult> Delete(int id)
        {
            if (await reviewService.ReviewIsFromTheUser(id, UserId()) == false)
            {
                return BadRequest();
            }
            await reviewService.Delete(id);
            
            return RedirectToAction(nameof(MyReviews));
        }

        public async Task<IActionResult> Show(int id) => View(await reviewService.Show(id));

        private string UserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
