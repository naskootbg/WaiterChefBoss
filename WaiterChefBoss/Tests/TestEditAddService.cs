using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WaiterChefBoss.Contracts;
using WaiterChefBoss.Data;
using WaiterChefBoss.Data.Models;
using WaiterChefBoss.Models;
using WaiterChefBoss.Services;
using WaiterChefBoss.Services.Category;
using WaiterChefBoss.Services.Product;

namespace WaiterChefBoss.Tests
{
    public class TestEditAddService
    {
        public class TestReviewService
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
                        .UseInMemoryDatabase(databaseName: "EditAdd") // Give a Unique name to the DB
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
            public void AddCategoryTest()
            {
                ICategoryService catService = new CategoryService(this.context, null);
                IProductService prodcatService = new ProductService(this.context);
                IEditAddService service = new EditAddService(this.context, catService, null, prodcatService);


                CategoryViewModelService cat = new() { Id = 3, Name = "Category 3 name", Description = "Categorydescription 3", Status = 1 };

                service.AddCategory(cat);
                Assert.That(catService.CategoryExists(3).Result == true);
                Assert.That(catService.AllCategories().Result.Count() == 3);

            }
            [Test]

            public void EditCategoryGetTest()
            {
                ICategoryService catService = new CategoryService(this.context, null);
                IProductService prodcatService = new ProductService(this.context);
                IEditAddService service = new EditAddService(this.context, catService, null, prodcatService);

                var result = service.EditCategory(null, 3).Result;
                Assert.That(result.Name == "Category 3 name");

            }

            [Test]

            public void EditCategoryPostTest()
            {
                ICategoryService catService = new CategoryService(this.context, null);
                IProductService prodcatService = new ProductService(this.context);
                IEditAddService service = new EditAddService(this.context, catService, null, prodcatService);
                CategoryViewModelService cat = new() { Id = 3, Name = "Category 3 name edited", Description = "Categorydescription 3", Status = 1 };

                var result = service.EditCategory(cat, 3).Result;
                Assert.That(result.Name == "Category 3 name edited");

            }
            [Test]
            public void DeleteCategoryTest()
            {
                ICategoryService catService = new CategoryService(this.context, null);
                IProductService prodcatService = new ProductService(this.context);
                IEditAddService service = new EditAddService(this.context, catService, null, prodcatService);

                service.DeleteCategory(1);
                Assert.That(catService.CategoryExists(1).Result == false);
                Assert.That(catService.AllCategories().Result.Count() == 2);
            }
            [Test]


            public void AddProductTest()
            {
                ICategoryService catService = new CategoryService(this.context, null);
                IProductService prodcatService = new ProductService(this.context);
                IEditAddService service = new EditAddService(this.context, catService, null, prodcatService);
                var prod = new ProductFormViewModel() { Id = 4, Name = "test 4 name", Description = "description 4", Status = 1, CategoryId = 2, TimeCooking = 0, Weight = 0, Price = 3.00 };
                service.AddProduct(prod);
                Assert.That(prodcatService.ProductExists(4).Result == true);
                Assert.That(prodcatService.AllProducts().Result.Count() == 4);

            }
            [Test]

            public void EditProductGetTest()
            {
                ICategoryService catService = new CategoryService(this.context, null);
                IProductService prodcatService = new ProductService(this.context);
                IEditAddService service = new EditAddService(this.context, catService, null, prodcatService);
                var result = service.EditProduct(null, 3).Result;
                Assert.That(result.Id == 3);
            }
            [Test]

            public void EditProductPostTest()
            {
                ICategoryService catService = new CategoryService(this.context, null);
                IProductService prodcatService = new ProductService(this.context);
                IEditAddService service = new EditAddService(this.context, catService, null, prodcatService);
                var prod = new ProductFormViewModel() { Id = 4, Name = "test 4 name edited", Description = "description 4", Status = 1, CategoryId = 2, TimeCooking = 0, Weight = 0, Price = 3.00 };
                var result = service.EditProduct(prod, 4).Result;
                Assert.That(result.Name == "test 4 name edited");
            }

            [Test]

            public void DeleteProductTest()
            {
                ICategoryService catService = new CategoryService(this.context, null);
                IProductService prodcatService = new ProductService(this.context);
                IEditAddService service = new EditAddService(this.context, catService, null, prodcatService);

                service.DeleteProduct(1);
                var res1 = prodcatService.ProductExists(1).Result;
                Assert.That(res1 == false);
                var res2 = prodcatService.AllProducts().Result.Count();
                Assert.That(res2 == 2);  //???????????
            }

        }
    }
}