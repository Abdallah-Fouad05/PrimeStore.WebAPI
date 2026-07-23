using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrimeStore.data.Entities.Status;

namespace PrimeStore.infrastructure.Configurations
{
    public class UserStatusConfigurations : IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder.HasData
            (
                    new UserStatus { StatusId = 1, Name = "Active" },
                    new UserStatus { StatusId = 2, Name = "Pending" },
                    new UserStatus { StatusId = 3, Name = "Block" }
            );
        }
    }
}
