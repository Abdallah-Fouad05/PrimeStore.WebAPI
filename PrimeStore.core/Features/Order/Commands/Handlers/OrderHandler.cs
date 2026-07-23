using MediatR;
using PrimeStore.core.Features.Order.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Order.Commands.Handlers
{
    public class OrderHandler : IRequestHandler<UpdateOrderStatusCommand, Response<string>>
    {
        #region Fields
        private readonly IOrderService _OrderService;
        #endregion

        #region Constructor
        public OrderHandler(IOrderService orderService)
        {
            _OrderService = orderService;
        }
        #endregion
        public async Task<Response<string>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var response = await _OrderService.UpdateOrderStatus(request.OrderId, (int)request.OrderStatus);

            if (response == ResultString.NotFound)
            {
                return ResponseHandler.NotFound<string>(response);
            }

            return ResponseHandler.Success<string>(response);
        }
    }
}
