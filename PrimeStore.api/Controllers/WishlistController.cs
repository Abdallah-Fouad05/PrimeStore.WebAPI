using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Api.Base;
using PrimeStore.core.Features.Wishlist.Commands.Models;
using PrimeStore.core.Features.Wishlist.Queries.Models;
using PrimeStore.core.Features.Wishlist.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.Core.Wrappers;
using static PrimeStore.Data.AppMetaData.Router;

namespace PrimeStore.api.Controllers
{
    public class WishlistController : AppControllerBase
    {
        [Authorize]
        [HttpGet(WishlistRouting.UserWishlistPaginated)]
        [ProducesResponseType(typeof(PaginatedResult<GetUserwishlistPaginatedListResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserWishlistPaginated([FromQuery] GetUserWishlistpaginatedListQuery request)
        {
            var response = await _Mediator.Send(request);
            return Ok(response);
        }

        [Authorize]
        [HttpPost(WishlistRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProductToUseWishlist([FromBody] AddWishlistCommand request)
        {
            var response = await _Mediator.Send(request);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete(WishlistRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveProductFromUseWishlist([FromQuery] int WishlistId)
        {
            var response = await _Mediator.Send(new DeleteWishlistCommand(WishlistId));
            return Ok(response);
        }


        [HttpGet(WishlistRouting.IsProductInUserWishlist)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> IsProductInUserWishlist([FromBody] IsProductInUserWishlistQuery request)
        {
            var response = await _Mediator.Send(request);
            return Ok(response);
        }

    }
}
