using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrimeStore.data.Entities;

namespace PrimeStore.infrastructure.Configurations
{
    public class CartItemConfigurations : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(x => x.CartItemId);
            builder.Property(x => x.CartItemId).ValueGeneratedOnAdd();

            builder.HasIndex(x => new { x.CartId, x.ProductId }).IsUnique();
        }
    }
}
