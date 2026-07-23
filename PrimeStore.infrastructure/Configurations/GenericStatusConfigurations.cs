using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrimeStore.data.Entities.Status;

namespace PrimeStore.infrastructure.Configurations
{
    public class GenericStatusConfigurations : IEntityTypeConfiguration<GenericStatus>
    {
        public void Configure(EntityTypeBuilder<GenericStatus> builder)
        {
            builder.HasData
                (
                    new GenericStatus { StatusId = 1, StatusName = "Active" },
                    new GenericStatus { StatusId = 2, StatusName = "InActive" }
                );
        }
    }
}
