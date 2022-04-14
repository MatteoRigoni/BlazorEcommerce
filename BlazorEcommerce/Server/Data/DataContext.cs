using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "The Lord of the Rings",
                    Description = "The title refers to the story's main antagonist, the Dark Lord Sauron",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4d/TREEBEARD.jpg/225px-TREEBEARD.jpg",
                    Price = 10.19m
                },
                 new Product
                 {
                     Id = 2,
                     Title = "The Chronicles of Narnia",
                     Description = " The series is set in the fictional realm of Narnia",
                     ImageUrl = "https://upload.wikimedia.org/wikipedia/en/c/cb/The_Chronicles_of_Narnia_box_set_cover.jpg",
                     Price = 20.99m
                 });
        }

        public DbSet<Product> Products { get; set; }
    }
}
