using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Status;

namespace PrimeStore.core.Features.Payments.Commands.Models
{
    public class UpdatePaymentStatusCommand : IRequest<Response<string>>
    {
        public int PaymentId { get; set; }

        public PaymentStatusEnum Status { get; set; }

    }
}
