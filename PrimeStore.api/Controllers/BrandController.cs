using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Api.Base;
using PrimeStore.core.Features.Brands.Commands.Models;
using PrimeStore.core.Features.Brands.Queries.Models;
using PrimeStore.core.Features.Brands.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Role;
using static PrimeStore.Data.AppMetaData.Router;

namespace PrimeStore.api.Controllers
{
    public class BrandController : AppControllerBase
    {
        /// <summary>
        /// [Admin]
        /// </summary>

        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpGet(BrandRouting.List)]
        [ProducesResponseType(typeof(Response<List<GetBrandListResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBrandList()
        {
            var response = await _Mediator.Send(new GetBrandListQuery());
            return NewResult(response);
        }

        [HttpGet(BrandRouting.ActiveList)]
        [ProducesResponseType(typeof(Response<List<GetBrandListResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveBrandList()
        {
            var response = await _Mediator.Send(new GetActiveBrandListQuery());
            return NewResult(response);
        }

        [HttpGet(BrandRouting.GetByID)]
        [ProducesResponseType(typeof(Response<GetBrandByIdResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBrandById([FromQuery] GetBrandByIdQuery request)
        {
            var response = await _Mediator.Send(request);

            return NewResult(response);
        }

        /// <summary>
        /// [Admin]
        /// </summary>

        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpPost(BrandRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBrand([FromBody] AddBrandCommand request)
        {
            var response = await _Mediator.Send(request);

            return NewResult(response);
        }

        /// <summary>
        /// [Admin]
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpPut(BrandRouting.Edit)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditBrand([FromBody] EditBrandCommand request)
        {
            var response = await _Mediator.Send(request);

            return NewResult(response);
        }

        /// <summary>
        /// [Admin]
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpDelete(BrandRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBrand([FromQuery] int BrandId)
        {
            var response = await _Mediator.Send(new DeleteBrandCommand(BrandId));

            return NewResult(response);
        }

    }
}
