using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Api.Base;
using PrimeStore.core.Features.Authorization.Commands.Models;
using PrimeStore.core.Features.Authorization.Queries.Models;
using PrimeStore.core.Features.Users.Commands.Models;
using PrimeStore.core.Features.Users.Queries.Models;
using PrimeStore.core.Features.Users.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.Core.Wrappers;
using PrimeStore.data.Helper.Role;
using PrimeStore.data.Results;
using static PrimeStore.Data.AppMetaData.Router;
namespace PrimeStore.api.Controllers
{
    public class UserController : AppControllerBase
    {
        /// <summary>
        /// [Admin]
        /// </summary>
        [Authorize(Roles = $"{nameof(UserRoleEnum.ADMIN)}")]
        [HttpGet(UserRouting.Paginated)]
        [ProducesResponseType(typeof(PaginatedResult<GetUserPaginatedListResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUserPaginatedList([FromQuery] GetUserPaginatedListQuery request)
        {
            var response = await _Mediator.Send(request);
            return Ok(response);
        }

        [Authorize]
        [HttpGet(UserRouting.GetById)]
        [ProducesResponseType(typeof(Response<GetUserByIdResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUserById([FromQuery] int UserId)
        {
            var response = await _Mediator.Send(new GetUserByIdQuery(UserId));
            return Ok(response);
        }


        [HttpPost(UserRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddUser([FromBody] AddUserCommand command)
        {
            var response = await _Mediator.Send(command);
            return NewResult(response);
        }

        [Authorize]
        [HttpPut(UserRouting.Edit)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditUser([FromBody] EditUserCommand command)
        {
            var response = await _Mediator.Send(command);
            return Ok(response);
        }

        [Authorize]
        [HttpGet(UserRouting.UserRoles)]
        [ProducesResponseType(typeof(Response<ManageUserRoleResult>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUserRoles([FromQuery] int UserId)
        {
            var response = await _Mediator.Send(new ManageUserRoleQuery(UserId));
            return Ok(response);
        }

        #region tr
        //[Authorize]
        //[HttpGet(UserRouting.UserClaims)]
        //public async Task<ActionResult> GetUserClaims([FromQuery] int UserId)
        //{
        //    var response = await _Mediator.Send(new ManageUserClaimQuery(UserId));
        //    return Ok(response);
        //}
        #endregion

        /// <summary>
        /// [Owner]
        /// </summary>

        [Authorize(Roles = $"{nameof(UserRoleEnum.OWNER)}")]
        [HttpPut(UserRouting.EditUserRoles)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditUserRoles([FromBody] UpdateUserRolesCommand command)
        {
            var response = await _Mediator.Send(command);
            return Ok(response);
        }

        #region tr2
        //[Authorize(Roles = $"{nameof(UserRoleEnum.OWNER)}")]
        //[HttpPut(UserRouting.EditUserClaims)]
        //public async Task<ActionResult> EditUserClaims([FromBody] UpdateUserClaimsCommand command)
        //{
        //    var response = await _Mediator.Send(command);
        //    return Ok(response);
        //}
        #endregion

        /// <summary>
        /// [Owner]
        /// </summary>

        [Authorize(Roles = $"{nameof(UserRoleEnum.OWNER)}")]
        [HttpPut(UserRouting.EditUserStatus)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditUserStatuss([FromBody] UpdateUserStatusCommand command)
        {
            var response = await _Mediator.Send(command);
            return Ok(response);
        }
    }
}
