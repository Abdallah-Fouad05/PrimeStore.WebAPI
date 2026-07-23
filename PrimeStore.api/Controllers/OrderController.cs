using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Api.Base;
using PrimeStore.core.Features.Order.Commands.Models;
using PrimeStore.core.Features.Order.Queries.Models;
using PrimeStore.core.Features.Order.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Role;
using PrimeStore.data.Helper.Status;
using static PrimeStore.Data.AppMetaData.Router;

namespace PrimeStore.api.Controllers
{
    public class OrderController : AppControllerBase
    {
        [Authorize]
        [HttpGet(OrderRouting.GetUserOrders)]
        [ProducesResponseType(typeof(Response<GetUserOrdersByUserIdResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserOrders([FromQuery] int userId, [FromQuery] OrderStatusEnum statusId)
        {
            var result = await _Mediator.Send(new GetUserOrdersByUserIdQuery { UserId = userId, OrderStatus = statusId });
            return NewResult(result);
        }

        [Authorize]
        [HttpGet(OrderRouting.GetOrderItems)]
        [ProducesResponseType(typeof(Response<GetOrderItemsByOrderIdResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderItems([FromQuery] int orderId)
        {
            var result = await _Mediator.Send(new GetOrderItemsByOrderIdQuery(orderId));
            return NewResult(result);
        }

        /// <summary>
        /// [Admin]
        /// </summary>
        [Authorize(Roles = nameof(UserRoleEnum.ADMIN))]
        [HttpPut(OrderRouting.EditUserOrderStatus)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusCommand command)
        {
            var result = await _Mediator.Send(command);
            return NewResult(result);
        }
    }
}
