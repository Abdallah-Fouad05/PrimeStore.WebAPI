using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Order.Queries.Models;
using PrimeStore.core.Features.Order.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Order.Queries.Handlers
{
    public class OrderHandler : IRequestHandler<GetOrderItemsByOrderIdQuery, Response<List<GetOrderItemsByOrderIdResponse>>>,
                                IRequestHandler<GetUserOrdersByUserIdQuery, Response<List<GetUserOrdersByUserIdResponse>>>
    {
        #region Fields
        private readonly IOrderService _OrderService;
        private readonly IMapper _Mapper;
        #endregion

        #region Constructor
        public OrderHandler(IOrderService orderService, IMapper mapper)
        {
            _OrderService = orderService;
            _Mapper = mapper;
        }

        #endregion

        public async Task<Response<List<GetOrderItemsByOrderIdResponse>>> Handle(GetOrderItemsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var orderItems = await _OrderService.GetOrderItemsByOrderId(request.OrderId);

            var orderItemsMapping = _Mapper.Map<List<GetOrderItemsByOrderIdResponse>>(orderItems);

            return ResponseHandler.Success(orderItemsMapping);
        }

        public async Task<Response<List<GetUserOrdersByUserIdResponse>>> Handle(GetUserOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _OrderService.GetUserOrdersByUserId(request.UserId, (int)request.OrderStatus);

            var orderItemsMapping = _Mapper.Map<List<GetUserOrdersByUserIdResponse>>(orders);

            return ResponseHandler.Success(orderItemsMapping);
        }
    }
}
