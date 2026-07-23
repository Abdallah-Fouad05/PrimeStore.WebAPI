using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Api.Base;
using PrimeStore.core.Features.Products.Commands.Models;
using PrimeStore.core.Features.Products.Queries.Models;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.Core.Wrappers;
using PrimeStore.data.Helper.Role;
using static PrimeStore.Data.AppMetaData.Router;

namespace PrimeStore.api.Controllers
{
    [ApiController]
    public class ProductController : AppControllerBase
    {
        /// <summary>
        /// [Admin]
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpGet(ProductRouting.List)]
        [ProducesResponseType(typeof(Response<List<GetProductListResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductList()
        {
            var response = await _Mediator.Send(new GetProductListQuery());

            return NewResult(response);
        }

        /// <summary>
        /// [Admin]
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpGet(ProductRouting.Paginated)]
        [ProducesResponseType(typeof(PaginatedResult<GetProductListResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductPaginatedList([FromQuery] GetProductPaginatedListQuery request)
        {
            var response = await _Mediator.Send(request);

            return Ok(response);
        }


        [HttpGet(ProductRouting.ActivePaginated)]
        [ProducesResponseType(typeof(PaginatedResult<GetProductListResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveProductPaginatedList([FromQuery] GetActiveProductPaginatedListQuery request)
        {
            var response = await _Mediator.Send(request);

            return Ok(response);
        }

        [HttpGet(ProductRouting.PaginatedByBrandId)]
        [ProducesResponseType(typeof(PaginatedResult<GetProductListResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductPaginatedListByBrandId([FromQuery] GetProductPaginatedListByBrandIdQuery request)
        {
            var response = await _Mediator.Send(request);

            return Ok(response);
        }

        [HttpGet(ProductRouting.PaginatedByCategoryId)]
        [ProducesResponseType(typeof(PaginatedResult<GetProductListResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductPaginatedListByBrandId([FromQuery] GetProductPaginatedListByCategoryIdQuery request)
        {
            var response = await _Mediator.Send(request);

            return Ok(response);
        }

        [HttpGet(ProductRouting.GetByID)]
        [ProducesResponseType(typeof(Response<GetProductByIdResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductById([FromQuery] int ProductId)
        {
            var response = await _Mediator.Send(new GetProductByIdQuery(ProductId));

            return NewResult(response);
        }

        /// <summary>
        /// [Admin]
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpPost(ProductRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductCommand product)
        {
            var response = await _Mediator.Send(product);

            return NewResult(response);
        }

        /// <summary>
        /// [Admin]
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpPut(ProductRouting.Edit)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditProduct([FromBody] EditProductCommand product)
        {
            var response = await _Mediator.Send(product);

            return NewResult(response);
        }

        /// <summary>
        /// [Admin]
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpDelete(ProductRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct([FromQuery] int ProductId)
        {
            var response = await _Mediator.Send(new DeleteProductCommand(ProductId));

            return NewResult(response);
        }

    }
}
