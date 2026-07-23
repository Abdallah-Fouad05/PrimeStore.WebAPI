using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper;

namespace PrimeStore.core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
