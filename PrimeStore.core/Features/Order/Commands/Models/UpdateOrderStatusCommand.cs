using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Status;

namespace PrimeStore.core.Features.Order.Commands.Models
{
    public class UpdateOrderStatusCommand : IRequest<Response<string>>
    {
        public int OrderId { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
