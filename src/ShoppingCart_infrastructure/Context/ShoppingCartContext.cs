using Microsoft.EntityFrameworkCore;
using ShoppingCart_Domain.Entities;
using ShoppingCart_infrastructure.Context.Configs;

namespace ShoppingCart_infrastructure.Context
{
    public class ShoppingCartContext : DbContext
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
        : base(options)
        { }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ShoppingCartConfig().Configure(modelBuilder.Entity<ShoppingCart>());
            new ProductConfig().Configure(modelBuilder.Entity<Product>());
        }
    }
}
