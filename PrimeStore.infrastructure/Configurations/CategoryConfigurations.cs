using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace PrimeStore.infrastructure.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.CategoryId);
            builder.Property(x => x.CategoryId).ValueGeneratedOnAdd();

            builder.Property(x => x.CategoryName).IsRequired().HasMaxLength(500);
            builder.HasIndex(x => x.CategoryName).IsUnique();

            builder.HasMany(x => x.ChildCategories).WithOne(x => x.ParentCategory)
                .HasForeignKey(x => x.ParentCategoryID).OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
