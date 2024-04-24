using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WaiterChefBoss.Data.Models;

namespace WaiterChefBoss.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.EnableSensitiveDataLogging();
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            const string USER_ID = "22e40406-8a9d-2d82-912c-5d6a640ee696";
             


            // Add a user to be added to the admin role
            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = USER_ID,
                UserName = "dump@dump.dump",
                NormalizedUserName = "DUMP@DUMP.DUMP",
                Email = "dump@dump.dump",
                NormalizedEmail = "DUMP@DUMP.DUMP",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEIB/N9AG5QrJ4XU3szWuwqgqG7qQ8CMr9dzz3f9F1lB84j0CxarXMAvnA6i0Exj/7Q==",
                SecurityStamp = "I5MOLV6IDX2DRGZMNIQ6KEUQKW3QIG3A",
                ConcurrencyStamp = "c4736b7b-4dcf-be6b-8b03-e299b4836146"
            });
             
            builder
                .Entity<Order>()
                .HasMany(o => o.OrderProducts)
                .WithOne(o => o.Order)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<Order>()
               .HasData(new Order()
               {
                   Id = 1,
                   Status = 0,
                   UserId = USER_ID,
                   DateAdded = DateTime.MinValue,
                   Table = 1,
                   Total = 0
               } 
               );
            builder
               .Entity<Discount>()
               .HasData(new Discount()
               {
                   Id = 1,
                   Percent = 5,                  
                   Total = 100.00
               },
                new Discount()
                {
                    Id = 2,
                    Percent = 6,
                    Total = 200.00
                }
               );
            builder
               .Entity<Category>()
               .HasData(new Category()
               {
                   Id = 1,
                   Name = "Fast Food",
                   Status = 1
               },
               new Category()
               {
                   Id = 2,
                   Name = "Dinner",
                   Status = 1
               },
               new Category()
               {
                   Id = 3,
                   Name = "Hot Drinks",
                   Status = 1
               },
               new Category()
               {
                   Id = 4,
                   Name = "Alchohol",
                   Status = 3
               });
    
            builder
               .Entity<Review>()
               .HasData(new Review()
               {
                   Id = 1,
                   Title = "Good Pizza",
                   Description = "Comes slowly",
                   Stars = 4,
                   ProductId = 1,
                   UserId = USER_ID
               },
               new Review()
               {
                   Id = 2,
                   Title = "Excelent Pizza",
                   Description = "Will Recommend",
                   Stars = 5,
                   ProductId = 1,
                   UserId = USER_ID
               },
               new Review()
               {
                   Id = 3,
                   Title = "Great Pizza",
                   Description = "Will buy again",
                   Stars = 5,
                   ProductId = 1,
                   UserId = USER_ID
               },
               new Review()
               {
                   Id = 4,
                   Title = "Where my beer is",
                   Description = "Going home",
                   Stars = 1,
                   ProductId = 4,
                   UserId = USER_ID
               },
               new Review()
               {
                   Id = 5,
                   Title = "Hot",
                   Description = "I love shkembe, but too hot",
                   Stars = 3,
                   ProductId = 2,
                   UserId = USER_ID
               }, new Review()
               {
                   Id = 6,
                   Title = "Cold",
                   Description = "I love shkembe, but too cold",
                   Stars = 4,
                   ProductId = 2,
                   UserId = USER_ID
               }) ;

            builder
       .Entity<Product>()
       .HasData(new Product()
       {
           Id = 1,
           Name = "Pizza",
           Description = "Refrigerated Pillsburytm Classic Crust Pizza Crust\r\nLean Ground Beef\r\nBell Pepper (thin strips, yellow, red and green)\r\nOnion (thinly sliced)\r\nGarlic-Pepper Blend\r\nPizza Sauce\r\nItalian Cheese Blend (shredded)",
           CategoryId = 1,
           Status = 1,
           TimeCooking = 10,
           Weight = 0.5,
           ImageUrl = "https://thumbs.dreamstime.com/z/pepperoni-pizza-thinly-sliced-popular-topping-american-style-pizzerias-30402134.jpg",
           Price = 25.97,
           Calories= "555 cal",
           
       
       },
       new Product()
       {
           Id = 2,
           Name = "Shkembe chorba",
           Description = "Kg. tripe (veal)\r\nMilk\r\nsweet paprika\r\nCayenne pepper\r\nSalt to taste\r\ngarlic\r\nVinegar\r\nOil\r\nbutter"
,
           CategoryId = 2,
           Status = 1,
           TimeCooking = 15,
           Weight = 0.5,
           ImageUrl = "https://thumbs.dreamstime.com/b/soup-3843446.jpg",
           Price = 2.97,
           Calories = "551 cal",
          
       },
       new Product()
       {
           Id = 3,
           Name = "Coffee",
           Description = "Hot coffee with very special taste of coffee",
           CategoryId = 3,
           Status = 1,
           TimeCooking = 5,
           Weight = 0.05,
           ImageUrl = "https://thumbs.dreamstime.com/b/coffee-concept-fried-coffee-beans-porcelain-white-coffee-cup-coffee-concept-fried-coffee-beans-porcelain-white-coffee-cup-113807195.jpg",
           Price = 2,
           Calories = "44 cal",
           
       },
       new Product()
       {
           Id = 4,
           Name = "Beer",
           Description = "Cold natural beer, but maybe alchohol",
           CategoryId = 4,
           Status = 1,
           TimeCooking = 1,
           Weight = 0.5,
           ImageUrl = "https://thumbs.dreamstime.com/b/beer-959519.jpg",
           Price = 5.97,
           Calories = "5555 cal",
           
       });

            base.OnModelCreating(builder);
        }
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderProducts> OrdersProducts { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Discount> Discounts { get; set; } = null!;


    }
}