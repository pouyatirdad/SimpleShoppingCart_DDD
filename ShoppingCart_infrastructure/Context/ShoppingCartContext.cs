using Microsoft.EntityFrameworkCore;
using ShoppingCart_Domain.Entities;
using ShoppingCart_Domain.ValueObjects;
using ShoppingCart_infrastructure.Context.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart_infrastructure.Context
{
    public class ShoppingCartContext: DbContext
    {
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ShoppingCartConfig().Configure(modelBuilder.Entity<ShoppingCart>());
            new ProductConfig().Configure(modelBuilder.Entity<Product>());
        }
    }
}
