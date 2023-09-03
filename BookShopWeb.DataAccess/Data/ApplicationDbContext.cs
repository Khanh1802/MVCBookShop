using BookShopWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShopWeb.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Product>().HasData(
            //    new Product
            //    {
            //        Id = Guid.NewGuid(),
            //        Title = "Forturn of Time",
            //        Author = "Billy Spark",
            //        Description = "Praesent vitae sodafes libero",
            //        ISBN = "SWD9999001",
            //        ListPrice = 99,
            //        Price = 90,
            //        Price50 = 85,
            //        Price100 = 80,
            //        CategoryId = 
            //    }
            //    );
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
