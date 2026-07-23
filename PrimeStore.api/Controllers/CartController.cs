using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Api.Base;
using PrimeStore.core.Features.Cart.Commands.Models;
using PrimeStore.core.Features.Cart.Queries.Models;
using PrimeStore.core.Features.Cart.Queries.Results;
using PrimeStore.Core.Bases;
using static PrimeStore.Data.AppMetaData.Router;

namespace PrimeStore.api.Controllers
{
    public class CartController : AppControllerBase
    {
        [Authorize]
        [HttpPost(CartRouting.AddCartItem)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartItemCommand request)
        {
            var response = await _Mediator.Send(request);
            return NewResult(response);
        }

        [Authorize]
        [HttpPut(CartRouting.EditCartItem)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditCartItem([FromBody] UpdateCartItemCommand request)
        {
            var response = await _Mediator.Send(request);
            return NewResult(response);
        }

        [Authorize]
        [HttpPost(CartRouting.DeleteCartItem)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCartItem([FromRoute] int CartItemId, int UserId)
        {
            var response = await _Mediator.Send(new DeleteCartItemCommand(CartItemId, UserId));
            return NewResult(response);
        }

        [Authorize]
        [HttpGet(CartRouting.GetUserCartItems)]
        [ProducesResponseType(typeof(Response<List<GetUserCartItemsListResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserCartItems([FromBody] int UserId)
        {
            var response = await _Mediator.Send(new GetUserCartItemsListQuery(UserId));
            return NewResult(response);
        }


    }
}
