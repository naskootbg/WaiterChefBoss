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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder
            //    .Entity<Order>()
            //    .HasMany(o => o.Products)
            //    .WithOne(o => o.Order)
            //    .OnDelete(DeleteBehavior.NoAction);
            
             
            builder
               .Entity<Category>()
               .HasData(new Category()
               {
                   Id = 1,
                   Name = "Fast Food"
               },
               new Category()
               {
                   Id = 2,
                   Name = "Dinner"
               },
               new Category()
               {
                   Id = 3,
                   Name = "Hot Drinks"
               },
               new Category()
               {
                   Id = 4,
                   Name = "Alchohol"
               });

               builder
       .Entity<Product>()
       .HasData(new Product()
       {
           Id = 1,
           Name = "Pizza",
           Description = "Pizza with meal and chease",
           CategoryId = 1,
           Status = 1,
           TimeCooking = 10,
           Weight = 0.5M,
           ImageUrl = "https://thumbs.dreamstime.com/z/pepperoni-pizza-thinly-sliced-popular-topping-american-style-pizzerias-30402134.jpg",
           Price = 25.97M,
           Calories= "555 cal"
       
       },
       new Product()
       {
           Id = 2,
           Name = "Shkembe chorba",
           Description = "Shkembe chorba with shkembe and a lot garlic and vinegar",
           CategoryId = 2,
           Status = 1,
           TimeCooking = 15,
           Weight = 0.5M,
           ImageUrl = "https://thumbs.dreamstime.com/b/soup-3843446.jpg",
           Price = 2.97M,
           Calories = "551 cal"
       },
       new Product()
       {
           Id = 3,
           Name = "Coffee",
           Description = "Hot coffee with very special taste of coffee",
           CategoryId = 3,
           Status = 1,
           TimeCooking = 5,
           Weight = 0.05M,
           ImageUrl = "https://thumbs.dreamstime.com/b/coffee-concept-fried-coffee-beans-porcelain-white-coffee-cup-coffee-concept-fried-coffee-beans-porcelain-white-coffee-cup-113807195.jpg",
           Price = 2M,
           Calories = "44 cal"
       },
       new Product()
       {
           Id = 4,
           Name = "Beer",
           Description = "Cold natural beer, but maybe alchohol",
           CategoryId = 4,
           Status = 1,
           TimeCooking = 1,
           Weight = 0.5M,
           ImageUrl = "https://thumbs.dreamstime.com/b/beer-959519.jpg",
           Price = 5.97M,
           Calories = "5555 cal"
       });

            base.OnModelCreating(builder);
        }
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderProducts> OrdersProducts { get; set; } = null!;

    }
}