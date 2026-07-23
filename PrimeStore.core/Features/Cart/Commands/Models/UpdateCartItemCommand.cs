using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Results;

namespace PrimeStore.core.Features.Cart.Commands.Models
{
    public class UpdateCartItemCommand : UpdateCartItemResult, IRequest<Response<string>>
    {

    }
}
