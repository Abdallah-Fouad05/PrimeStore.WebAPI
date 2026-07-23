using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper;

namespace PrimeStore.core.Features.Authenication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

    }
}
