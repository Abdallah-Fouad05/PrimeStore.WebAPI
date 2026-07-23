using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PrimeStore.Api.Base;
using PrimeStore.core.Features.Authenication.Commands.Models;
using PrimeStore.core.Features.Authentication.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper;
using static PrimeStore.Data.AppMetaData.Router;

namespace PrimeStore.api.Controllers
{
    public class AuthenticationController : AppControllerBase
    {
        [EnableRateLimiting("AuthLimiter")]
        [HttpPost(AuthenticationRouting.SignIn)]
        [ProducesResponseType(typeof(Response<JwtAuthResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand request)
        {
            var response = await _Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(AuthenticationRouting.RefreshToken)]
        [ProducesResponseType(typeof(Response<JwtAuthResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand request)
        {
            var response = await _Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(AuthenticationRouting.ConfirmEmail)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand request)
        {
            var response = await _Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(AuthenticationRouting.SendResetPasswordCode)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendResetPasswordCode([FromBody] SendResetPasswordCodeCommand request)
        {
            var response = await _Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(AuthenticationRouting.ConfirmResetPasswordCode)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] ConfirmResetPasswordCommand request)
        {
            var response = await _Mediator.Send(request);
            return NewResult(response);
        }

        [HttpPost(AuthenticationRouting.ResetPassword)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
        {
            var response = await _Mediator.Send(request);
            return NewResult(response);
        }
    }
}