using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;

namespace WaiterChefBoss.Services.Review
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext context;
        private readonly IMemoryCache cache;
        public ReviewService(ApplicationDbContext _context, IMemoryCache _cache)
        {
            cache = _cache;
            context = _context;
        }

        public async Task<IEnumerable<ReviewViewModel>> MyReviews(string userId)
        {
            var r = await context
                .Reviews
                .AsNoTracking()
                .Include(p => p.Product)
                .Where(r => r.UserId == userId)
                .Select(r => new ReviewViewModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Stars = r.Stars,
                    ProductId = r.ProductId,
                    ProductName = r.Product.Name
                })
                .ToListAsync();
             
            return r;
        }



        public async Task<IEnumerable<ReviewViewModel>> ProductReviews(int id, double average, int total)
        {
             
             
           
            var r = await context
                .Reviews
                .AsNoTracking()
                .Where(r => r.ProductId == id)
                .Select(r => new ReviewViewModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Stars = r.Stars,
                    ProductId = r.ProductId,
                    AverageStars = average,
                    TotalReviews = total,
                    ProductName = r.Product.Name
                     


                })
                .ToListAsync();
            return r;
        }
        public async Task<bool> ReviewIsFromTheUser(int id, string userId)
        {
            var user = await context
                .Reviews
                .AsNoTracking()
                .Where(r => r.Id == id && r.UserId == userId)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public async Task<int> Add(ReviewViewModel model, string userId)
        {
            var review = new Data.Models.Review()
            {
                Title = model.Title,
                UserId = userId,
                Description = model.Description,
                Stars = model.Stars,
                ProductId = model.ProductId 
                
            };
            await context.AddAsync(review);
            await context.SaveChangesAsync();
            return review.Id;
        }

        public async Task<IEnumerable<ReviewViewModel>> All()
        {
            var model = await context.Reviews
                .AsNoTracking()
                .Select(r =>new ReviewViewModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Stars = r.Stars,
                    ProductId = r.ProductId                   
                })
                .ToListAsync();
            return model;
        }

        public async Task Delete(int id)
        {
            var review = await context
                .Reviews.FindAsync(id);
            if (review != null)
            {
                context.Reviews.Remove(review);
                await context.SaveChangesAsync();
            }
            
        }

        public async Task<ReviewViewModel> Edit(int id, ReviewViewModel? m)
        {
            var review = await context
                .Reviews.FindAsync(id);
            if (review != null)
            {
                if (m == null)
                {
                    var model = new ReviewViewModel()
                    {
                        Id = review.Id,
                        Title = review.Title,
                        Description = review.Description,
                        Stars = review.Stars,
                        ProductId = review.ProductId,
                        UserId = review.UserId
                        
                    };
                    return model;
                    
                }
                else
                {
                    review.Id = m.Id;
                    review.Title = m.Title;
                    review.Description = m.Description;
                    review.Stars = m.Stars;
                    review.UserId = m.UserId;
                    review.ProductId = m.ProductId;
                    review.UserId = m.UserId;
                     
                    await context.SaveChangesAsync();
                    var model = new ReviewViewModel()
                    {
                        Title = m.Title,
                        Description = m.Description,
                        Stars = m.Stars
                    };
                    return model;

                }
            }
            else
            {
                return new ReviewViewModel();
            }
        }

        public async Task<ReviewViewModel> Show(int id)
        {
            var m = await context
                .Reviews.FindAsync(id);
            if (m != null)
            {
                return new ReviewViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    Stars = m.Stars,
                    UserId = m.UserId,
                    ProductId = m.ProductId
                };
            }
            else
            {
                return new ReviewViewModel();
            }
        }

        public async Task<double> AverageScore(int productId)
        {
            var reviews = await context.Reviews
                .AsNoTracking()
                .Where(r => r.ProductId == productId)
                .ToListAsync();

            int sum = 0;
            foreach (var item in reviews)
            {
                sum += item.Stars;
            }
            double count = await ProductReviewsCount(productId);
            return Math.Round(sum/count, 2);
        }
        public async Task<int> ProductReviewsCount(int id)
        {
            var count = await context.Reviews
                .AsNoTracking()
                .Where(r => r.ProductId == id)
                .ToListAsync();

            return count.Count();
        }

    }
}
