using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Api.Base;
using PrimeStore.core.Features.Categories.Commands.Models;
using PrimeStore.core.Features.Categories.Queries.Models;
using PrimeStore.core.Features.Categories.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Role;
using static PrimeStore.Data.AppMetaData.Router;

namespace PrimeStore.api.Controllers
{
    public class CategoryController : AppControllerBase
    {
        [HttpGet(CategoryRouting.List)]
        [ProducesResponseType(typeof(Response<List<GetCategoriesListResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryList()
        {
            var response = await _Mediator.Send(new GetCategoriesListQuery());

            return NewResult(response);
        }

        /// <summary>
        /// [Admin]
        /// </summary>

        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpPost(CategoryRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand request)
        {
            var response = await _Mediator.Send(request);

            return NewResult(response);
        }


        /// <summary>
        /// [Admin]
        /// </summary>

        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpPut(CategoryRouting.Edit)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditCategory([FromBody] EditCategoryCommand request)
        {
            var response = await _Mediator.Send(request);

            return NewResult(response);
        }

        /// <summary>
        /// [Admin]
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpDelete(CategoryRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCategory([FromQuery] int CategoryId)
        {
            var response = await _Mediator.Send(new DeleteCategoryCommand(CategoryId));

            return NewResult(response);
        }
    }
}
