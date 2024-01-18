using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart_Domain.Entities;

namespace ShoppingCart_infrastructure.Context.Configs
{
    public class ShoppingCartConfig : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Items);
            builder.OwnsOne(x => x.Total);
        }
    }
}
