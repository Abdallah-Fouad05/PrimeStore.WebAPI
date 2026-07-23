using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PrimeStore.core.Features.Authenication.Commands.Models;
using PrimeStore.core.Features.Authentication.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Entities.Identity;
using PrimeStore.data.Helper;
using PrimeStore.Service.Abstracts;


namespace PrimeStore.core.Features.Authenication.Commands.Handler
{
    public class AuthenticationHandler : IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
                                         IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>,
                                         IRequestHandler<ConfirmEmailCommand, Response<string>>,
                                         IRequestHandler<SendResetPasswordCodeCommand, Response<string>>,
                                         IRequestHandler<ConfirmResetPasswordCommand, Response<string>>,
                                         IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly UserManager<User> _UserManager;
        private readonly IMapper _Mapper;
        private readonly IAuthenticationService _AuthenticationService;
        #endregion

        #region Constructor
        public AuthenticationHandler(UserManager<User> userManager, IMapper mapper, IAuthenticationService authenticationService)
        {
            _UserManager = userManager;
            _Mapper = mapper;
            _AuthenticationService = authenticationService;
        }

        #endregion

        #region handler
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //find by username
            var userResult = await _UserManager.FindByNameAsync(request.UserName);
            if (userResult == null)
            {
                return ResponseHandler.BadRequest<JwtAuthResult>("Login failed. Invalid credentials.");
            }
            //checkpassword
            bool result = await _UserManager.CheckPasswordAsync(userResult, request.Password);
            if (!result)
            {
                return ResponseHandler.BadRequest<JwtAuthResult>("Login failed. Invalid credentials.");
            }
            //return success

            if (!userResult.EmailConfirmed)
            {
                return ResponseHandler.BadRequest<JwtAuthResult>("Email Not Confirmed");
            }


            //jwt Token
            var AccessToken = await _AuthenticationService.GetJWTToken(userResult);

            return ResponseHandler.Success(AccessToken);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _AuthenticationService.ReadJWTToken(request.AccessToken);

            var userIdAndExpireDate = await _AuthenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);

            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return ResponseHandler.Unauthorized<JwtAuthResult>("AlgorithmIsWrong");
                case ("TokenIsNotExpired", null): return ResponseHandler.Unauthorized<JwtAuthResult>("TokenIsNotExpired");
                case ("RefreshTokenIsNotFound", null): return ResponseHandler.Unauthorized<JwtAuthResult>("RefreshTokenIsNotFound");
                case ("RefreshTokenIsExpired", null): return ResponseHandler.Unauthorized<JwtAuthResult>("RefreshTokenIsExpired");
            }
            var (userId, expiryDate) = userIdAndExpireDate;

            var user = await _UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return ResponseHandler.NotFound<JwtAuthResult>();
            }

            var result = await _AuthenticationService.GetRefreshToken(user, jwtToken, expiryDate);

            return ResponseHandler.Success(result);
        }

        public async Task<Response<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var confirmEmail = await _AuthenticationService.ConfirmEmail(request.UserId, request.Code);
            if (confirmEmail == "ErrorWhenConfirmEmail")
                return ResponseHandler.BadRequest<string>("Error When Confirm Email");
            return ResponseHandler.Success<string>("Email confirmed successfully");
        }

        public async Task<Response<string>> Handle(SendResetPasswordCodeCommand request, CancellationToken cancellationToken)
        {
            var result = await _AuthenticationService.SendResetPasswordCode(request.Email);
            switch (result)
            {
                case "UserNotFound": return ResponseHandler.BadRequest<string>("UserIsNotFound");
                case "ErrorInUpdateUser": return ResponseHandler.BadRequest<string>("TryAgainInAnotherTime");
                case "Failed": return ResponseHandler.BadRequest<string>("TryAgainInAnotherTime");
                case "Success": return ResponseHandler.Success<string>("");
                default: return ResponseHandler.BadRequest<string>("TryAgainInAnotherTime");
            }
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _AuthenticationService.ConfirmResetPassword(request.Code, request.Email);
            switch (result)
            {
                case "UserNotFound": return ResponseHandler.BadRequest<string>("UserIsNotFound");
                case "Failed": return ResponseHandler.BadRequest<string>("InvaildCode");
                case "Success": return ResponseHandler.Success<string>("");
                default: return ResponseHandler.BadRequest<string>("InvaildCode");
            }
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _AuthenticationService.ResetPassword(request.Email, request.NewPassword);
            switch (result)
            {
                case "UserNotFound": return ResponseHandler.BadRequest<string>("UserIsNotFound");
                case "Failed": return ResponseHandler.BadRequest<string>("Faild Reset Password");
                case "Success": return ResponseHandler.Success<string>("");
                default: return ResponseHandler.BadRequest<string>("Try Again");
            }
        }

        #endregion
    }
}
