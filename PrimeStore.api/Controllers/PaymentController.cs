using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Api.Base;
using PrimeStore.core.Features.Payments.Commands.Models;
using PrimeStore.core.Features.Payments.Queries.Models;
using PrimeStore.core.Features.Payments.Queries.Result;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Role;
using static PrimeStore.Data.AppMetaData.Router;

namespace PrimeStore.api.Controllers
{
    public class PaymentController : AppControllerBase
    {
        [Authorize]
        [HttpGet(PaymentRouting.GetOrderPayment)]
        [ProducesResponseType(typeof(Response<GetPaymentByOrderIdResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaymentsByOrderId([FromQuery] int orderId)
        {
            var result = await _Mediator.Send(new GetPaymentByOrderIdQuery(orderId));

            return NewResult(result);
        }

        [Authorize]
        [HttpPost(PaymentRouting.AddPayment)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] AddPaymentCommand command)
        {
            var result = await _Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// [Admin]
        /// </summary>
        [Authorize(Roles = nameof(UserRoleEnum.ADMIN))]
        [HttpPut(PaymentRouting.EditPaymentStatus)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdatePaymentStatusCommand command)
        {
            var result = await _Mediator.Send(command);
            return Ok(result);
        }
    }
}
