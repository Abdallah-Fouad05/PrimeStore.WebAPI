using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Status;

namespace PrimeStore.core.Features.Authorization.Commands.Models
{
    public class UpdateUserStatusCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public UserStatusEnum UserStatus { get; set; }
    }
}
