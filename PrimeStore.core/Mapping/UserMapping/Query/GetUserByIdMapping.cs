using PrimeStore.core.Features.Users.Queries.Results;
using PrimeStore.data.Entities.Identity;

namespace PrimeStore.core.Mapping.UserMapping
{
    public partial class UserMapping
    {
        public void GetUserByIdMapping()
        {
            CreateMap<User, GetUserByIdResponse>()
                .ForPath(dest => dest.Status, opt => opt.MapFrom(src => src.UserStatus));


        }
    }
}
