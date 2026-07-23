using AutoMapper;

namespace PrimeStore.core.Mapping.UserMapping
{
    public partial class UserMapping : Profile
    {
        public UserMapping()
        {
            GetUserByIdMapping();
            GetUserPaginatedListMapping();
            AddUserMapping();
            EditUserMapping();
            UserStatusMapping();
        }
    }
}
