using PrimeStore.core.Features.Users.Commands.Models;
using PrimeStore.data.Entities.Identity;

namespace PrimeStore.core.Mapping.UserMapping
{
    public partial class UserMapping
    {
        public void EditUserMapping()
        {
            CreateMap<EditUserCommand, User>();
        }
    }
}
