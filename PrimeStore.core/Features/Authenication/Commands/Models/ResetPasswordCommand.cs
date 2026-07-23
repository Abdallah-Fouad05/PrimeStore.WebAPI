using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Authenication.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
