using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WaiterChefBoss.Data;
using WaiterChefBoss.Services;
using WaiterChefBoss.Models;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Services.Product;
using WaiterChefBoss.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WaiterChefBoss.Tests
{
    public class TestProductService
    {
        private ApplicationDbContext context;
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
            new Product(){ Id = 1, Name = "test 1 name", Description = "description 1", Status = 1, CategoryId = 1, TimeCooking =0, Weight= 0, Price = 1.00},
            new Product(){ Id = 2, Name = "test 2 name", Description = "description 2", Status = 1, CategoryId = 1, TimeCooking =0, Weight= 0, Price = 2.00 },
            new Product(){ Id = 3, Name = "test 3 name", Description = "description 3", Status = 1, CategoryId = 2, TimeCooking =0, Weight= 0, Price = 3.00 }

            };
            this.reviews = new List<Review>()
            {
            new Review(){ Id = 1, Title = "Review 1", Description = "Review description 1", Stars = 1, ProductId = 1, UserId ="dump1"},
            new Review(){ Id = 2, Title = "Review 2", Description = "Review description 2", Stars = 2, ProductId = 2, UserId ="dump2"}

            };
            this.op = new List<OrderProducts>()
            {
            new OrderProducts(){Id = 1, Status = 1, ProductId= 1 ,OrderId = 1 , UserId = "dump1" },
            new OrderProducts(){Id = 2, Status = 0, ProductId= 2 ,OrderId = 1 , UserId = "dump1" },
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
                    .UseInMemoryDatabase(databaseName: "Dump") // Give a Unique name to the DB
                    .Options;
            this.context = new ApplicationDbContext(options);
            this.context.AddRange(this.products);
            this.context.AddRange(this.reviews);
            this.context.AddRange(this.categories);
            this.context.AddRange(this.o);
            this.context.AddRange(this.op);
            this.context.SaveChanges();
        }



        [Test]
        public void ProductByIdTest()
        {
            IProductService service = new ProductService(this.context);
            var result = service.ProductById(1).Result;


            Assert.That(result.Name == "test 1 name");
        }
        [Test]
        public void AllProductsPerCategoryTest()
        {
            IProductService service = new ProductService(this.context);
            var result = service.AllProductsPerCategory(1).Result;
            Assert.That(result.Count() == 2);

        }
        [Test]
        public void AllProductsTest()
        {
            IProductService service = new ProductService(this.context);
            var result = service.AllProducts().Result;
            Assert.That(result.Count() == 3);
        }
        [Test]
        public void ProductExistsTest()
        {
            IProductService service = new ProductService(this.context);
            var resultTrue = service.ProductExists(1).Result;
            var resultFalse = service.ProductExists(10).Result;
            Assert.That(resultTrue);
            Assert.That(resultFalse == false);
        }
        [Test]
        public void ProductNameTest()
        {
            IProductService service = new ProductService(this.context);
            var result = service.ProductName(1).Result;
            Assert.That(result == "test 1 name");

        }
        [Test]
        public void ProductSearchTest()
        {
            IProductService service = new ProductService(this.context);
            var result = service.ProductSearch().Result;
            Assert.That(result.Count() == 3);
            Assert.That(result.FirstOrDefault().Name == "test 1 name");

        }
        [Test]

        public void ProductsInTheOrderTest()
        {
            IProductService service = new ProductService(this.context);
            var result = service.ProductsInTheOrder("dump1").Result;
            Assert.That(result.Count() == 1);
            Assert.That(result.FirstOrDefault().Name == "test 2 name");

        }

        [Test]
        public void AddandRemoveFromCartTest()
        {
            IProductService service = new ProductService(this.context);
            service.AddToCart("dump1", 2);
            var result = service.ProductsInTheOrder("dump1").Result;

            Assert.That(result.Count() == 2);
            Assert.That(result.FirstOrDefault().Name == "test 2 name");
            service.RemoveFromCart("dump1", 2);
            result = service.ProductsInTheOrder("dump1").Result;
            Assert.That(result.Count() == 1);
        }



    }
}
