using PrimeStore.core.Features.Users.Queries.Results;
using PrimeStore.data.Entities.Status;

namespace PrimeStore.core.Mapping.UserMapping
{
    public partial class UserMapping
    {
        public void UserStatusMapping()
        {
            CreateMap<UserStatus, UserStatusResponse>()
                    .ForMember(dest => dest.Id, otp => otp.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Name, otp => otp.MapFrom(src => src.Name));
        }
    }
}
