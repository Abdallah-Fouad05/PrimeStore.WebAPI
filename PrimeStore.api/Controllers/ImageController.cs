using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Api.Base;
using PrimeStore.core.Features.Images.Commands.Models;
using PrimeStore.core.Features.Images.Queries.Models;
using PrimeStore.core.Features.Images.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.Core.Wrappers;
using PrimeStore.data.Helper.Role;
using static PrimeStore.Data.AppMetaData.Router;

namespace PrimeStore.api.Controllers
{
    public class ImageController : AppControllerBase
    {

        [HttpGet(ImagesRouting.Paginated)]
        [ProducesResponseType(typeof(PaginatedResult<GetImagesPaginatedListResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetImagesListPaginated([FromQuery] GetImagesPaginatedListQuery request)
        {
            var response = await _Mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// [Admin]
        /// </summary>

        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpPost(ImagesRouting.AddImage)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage([FromForm] AddImageCommand request)
        {
            var response = await _Mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// [Admin]
        /// </summary>

        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpDelete(ImagesRouting.DeleteImage)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteImage([FromQuery] DeleteImageCommand request)
        {
            var response = await _Mediator.Send(request);
            return Ok(response);
        }

    }
}
