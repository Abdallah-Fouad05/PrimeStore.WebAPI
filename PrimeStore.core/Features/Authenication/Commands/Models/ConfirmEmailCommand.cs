using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Authenication.Commands.Models
{

    public class ConfirmEmailCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
