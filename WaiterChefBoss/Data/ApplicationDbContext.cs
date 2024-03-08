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

            base.OnModelCreating(builder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Boss> Bosses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Waiter> Waiters { get; set; }




    }
}