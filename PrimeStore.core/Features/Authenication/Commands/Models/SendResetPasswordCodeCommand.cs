using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Authenication.Commands.Models
{
    public class SendResetPasswordCodeCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public SendResetPasswordCodeCommand(string email)
        {
            Email = email;
        }
    }
}
