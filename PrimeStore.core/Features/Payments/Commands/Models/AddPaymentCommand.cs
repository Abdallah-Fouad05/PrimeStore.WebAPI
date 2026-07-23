using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Results;

namespace PrimeStore.core.Features.Payments.Commands.Models
{
    public class AddPaymentCommand : PaymentResult, IRequest<Response<string>>
    {

    }
}
