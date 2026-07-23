using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Authenication.Commands.Models
{
    public class ConfirmResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }

    }
}
