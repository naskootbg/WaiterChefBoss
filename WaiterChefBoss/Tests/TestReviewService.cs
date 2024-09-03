using NUnit.Framework;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Data;
using WaiterChefBoss.Models;
using Microsoft.EntityFrameworkCore;
using WaiterChefBoss.Services.Product;
using WaiterChefBoss.Services.Review;

namespace WaiterChefBoss.Tests
{
    public class TestReviewService
    {
        private ApplicationDbContext contextReviews;
        private IEnumerable<Review> reviews;
        private IEnumerable<OrderProducts> op;
        private IEnumerable<Order> o;
        private IEnumerable<Product> products;
        private IEnumerable<Category> categories;
        [OneTimeSetUp]
        public void TestInitialize()
        {
            this.categories = new List<Category>()
            {
            new Category(){Id = 1, Name = "Category 1 name", Description = "Category description 1", Status = 1 },
            new Category(){ Id = 2, Name = "Category 2 name", Description = "Categorydescription 2", Status = 1 }

            };
            this.products = new List<Product>()
            {
            new Product(){ Id = 1, Name = "test 1 name", Description = "description 1", Status = 1, TimeCooking =0, Weight= 0, Price = 1.00},
            new Product(){ Id = 2, Name = "test 2 name", Description = "description 2", Status = 1, TimeCooking =0, Weight= 0, Price = 2.00 },
            new Product(){ Id = 3, Name = "test 3 name", Description = "description 3", Status = 1, TimeCooking =0, Weight= 0, Price = 3.00 }

            };
            this.reviews = new List<Review>()
            {
            new Review(){ Id = 1, Title = "Review 1", Description = "Review description 1", Stars = 1, ProductId = 1, UserId ="dump1"},
            new Review(){ Id = 2, Title = "Review 2", Description = "Review description 2", Stars = 2, ProductId = 1, UserId ="dump2"}

            };
            this.op = new List<OrderProducts>()
            {
            new OrderProducts(){Id = 1, Status = 1, ProductId= 1 ,OrderId = 1 , UserId = "dump1" },
            new OrderProducts(){Id = 2, Status = 0, ProductId= 1 ,OrderId = 1 , UserId = "dump1" },
            new OrderProducts(){Id = 3, Status = 0, ProductId= 3 ,OrderId = 2 , UserId = "dump2" },

            };
            this.o = new List<Order>()
            {
            new Order(){Id = 1, Status = 1, Table= 1 ,Total = 12 , UserId = "dump1" },
            new Order(){Id = 2, Status = 2, Table= 2 ,Total = 123.87 , UserId = "dump1" },
            new Order(){Id = 3, Status = 3, Table=1 , UserId = "dump2" , Total = 23.45 },
            new Order(){Id = 4, Status = 4, Table=1 , UserId = "dump2" , Total = 23.45 },
            new Order(){Id = 5, Status = 5, Table=5 , UserId = "dump1" , Total = 23.45 },
            new Order(){Id = 6, Status = 0, Table=5 , UserId = "dump1" , Total = 23.45 },


            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "Review") // Give a Unique name to the DB
                    .Options;
            this.contextReviews = new ApplicationDbContext(options);
            this.contextReviews.AddRange(this.products);
            this.contextReviews.AddRange(this.reviews);
            this.contextReviews.AddRange(this.categories);
            this.contextReviews.AddRange(this.o);
            this.contextReviews.AddRange(this.op);
            this.contextReviews.SaveChanges();
        }
        [Test]
        public void AddTest()
        {
            IReviewService service = new ReviewService(this.contextReviews, null);
            var result = service.Add(new ReviewViewModel() { ProductId = 1, Title = "test", Description = "test ok", Stars = 3 }, "dump2").Result;


            Assert.That(result == 3);

        }
        [Test]
        public void AllTest()
        {
            IReviewService service = new ReviewService(this.contextReviews, null);
            var result = service.All().Result;

            Assert.That(result.FirstOrDefault().Title == "Review 1");
            Assert.That(result.Count() == 3);
        }
        [Test]
        public void AverageScoreTest()
        {
            IReviewService service = new ReviewService(this.contextReviews, null);
            var result = service.AverageScore(1).Result;
            Assert.That(result == 2);
        }
        [Test]
        public void DeleteTest()
        {
            IReviewService service = new ReviewService(this.contextReviews, null);

            service.Delete(1);
            var result = service.All().Result;

            Assert.That(result.Count() == 2);
        }
        [Test]
        public void EditTest()
        {
            IReviewService service = new ReviewService(this.contextReviews, null);
            var mymodel = new ReviewViewModel() { Id = 2, ProductId = 1, Title = "test", Description = "test ok", UserId = "dump1" };
            service.Edit(1, mymodel);
            var all = service.All().Result;
            Assert.That(all.LastOrDefault().Description == "test ok");


        }
        [Test]
        public void MyReviewsTest()
        {
            IReviewService service = new ReviewService(this.contextReviews, null);
            var result = service.MyReviews("dump2").Result;
            Assert.That(result.FirstOrDefault().Title == "Review 2");
            Assert.That(result.Count() == 2);

        }
        [Test]
        public void ProductReviewsTest()
        {
            IReviewService service = new ReviewService(this.contextReviews, null);
            var result = service.ProductReviews(1, 2, 5).Result;
            Assert.That(result.FirstOrDefault().Description == "Review description 2");
            Assert.That(result.FirstOrDefault().AverageStars == 2);
            Assert.That(result.FirstOrDefault().TotalReviews == 5);

        }
        [Test]
        public void ProductReviewsCountTest()
        {
            IReviewService service = new ReviewService(this.contextReviews, null);
            var result = service.ProductReviewsCount(1).Result;
            Assert.That(result == 2);

        }
        [Test]
        public void ReviewIsFromTheUserTest()
        {
            IReviewService service = new ReviewService(this.contextReviews, null);
            var result = service.ReviewIsFromTheUser(1, "dump2").Result;
            Assert.That(result == false);
            result = service.ReviewIsFromTheUser(2, "dump2").Result;
            Assert.That(result == true);
        }
        [Test]
        public void ShowTest()
        {
            IReviewService service = new ReviewService(this.contextReviews, null);
            var result = service.Show(1).Result;
            Assert.That(result.UserId == null);
            result = service.Show(2).Result;
            Assert.That(result.Description == "Review description 2");

        }
    }
}
