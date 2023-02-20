using Microsoft.EntityFrameworkCore;

namespace GuitarShop.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, Name = "Guitars" },
                new Category { CategoryID = 2, Name = "Basses" },
                new Category { CategoryID = 3, Name = "Drums" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1, 
                    CategoryID = 1,
                    Code = "strat",
                    Name = "Fender Stratocaster",
                    Price = (decimal)699.00
                },
                new Product
                {
                    ProductID = 2,
                    CategoryID = 1,
                    Code = "les_paul",
                    Name = "Gibson Les Paul",
                    Price = (decimal)1199.00
                },
                new Product
                {
                    ProductID = 3,
                    CategoryID = 1,
                    Code = "sg",
                    Name = "Gibson SG",
                    Price = (decimal)2517.00
                },
                new Product
                {
                    ProductID = 4,
                    CategoryID = 1,
                    Code = "fg700s",
                    Name = "Yamaha FG700S",
                    Price = (decimal)489.99
                },
                new Product
                {
                    ProductID = 5,
                    CategoryID = 1,
                    Code = "washburn",
                    Name = "Washburn D10S",
                    Price = (decimal)299.00
                },
                new Product
                {
                    ProductID = 6,
                    CategoryID = 1,
                    Code = "rodriguez",
                    Name = "Rodriguez Caballero 11",
                    Price = (decimal)415.00
                },
                new Product
                {
                    ProductID = 7,
                    CategoryID = 2,
                    Code = "precision",
                    Name = "Fender Precision",
                    Price = (decimal)799.99
                },
                new Product
                {
                    ProductID = 8,
                    CategoryID = 2,
                    Code = "hofner",
                    Name = "Hofner Icon",
                    Price = (decimal)499.99
                },
                new Product
                {
                    ProductID = 9,
                    CategoryID = 3,
                    Code = "ludwig",
                    Name = "Ludwig 5-piece Drum Set with Cymbals",
                    Price = (decimal)699.99
                },
                new Product
                {
                    ProductID = 10,
                    CategoryID = 3,
                    Code = "tama",
                    Name = "Tama 5-Piece Drum Set with Cymbals",
                    Price = (decimal)799.99
                }
            );
        }
    }
}