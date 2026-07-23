using MediatR;
using PrimeStore.core.Features.Order.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Status;

namespace PrimeStore.core.Features.Order.Queries.Models
{
    public class GetUserOrdersByUserIdQuery : IRequest<Response<List<GetUserOrdersByUserIdResponse>>>
    {
        public int UserId { get; set; }
        public OrderStatusEnum OrderStatus { get; set; } = 0;
    }
}
