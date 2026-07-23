using MediatR;
using PrimeStore.core.Features.Payments.Queries.Result;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Payments.Queries.Models
{
    public class GetPaymentByOrderIdQuery : IRequest<Response<List<GetPaymentByOrderIdResponse>>>
    {
        public int OrderId { get; set; }

        public GetPaymentByOrderIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
