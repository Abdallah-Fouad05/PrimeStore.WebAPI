using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrimeStore.data.Entities;

namespace PrimeStore.infrastructure.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.ProductId);
            builder.Property(x => x.ProductId).ValueGeneratedOnAdd();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
            builder.HasIndex(x => x.Title);

            builder.HasOne(x => x.Brand).WithMany(b => b.Products).HasForeignKey(x => x.BrandId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category).WithMany(b => b.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ProductImages).WithOne(b => b.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Reviews).WithOne(b => b.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ProductAttributes).WithOne(b => b.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Wishlists).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CartItems).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
