using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimeStore.core.Features.Users.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Entities.Identity;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Users.Commands.Handlers
{
    public class UserHandler : IRequestHandler<AddUserCommand, Response<string>>,
                               IRequestHandler<EditUserCommand, Response<string>>,
                               IRequestHandler<DeleteUserCommand, Response<string>>,
                               IRequestHandler<ChangeUserPasswordCommand, Response<string>>



    {
        #region Fields
        private readonly UserManager<User> _UserManager;
        private readonly IMapper _Mapper;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public readonly IUserService _UserService;
        private readonly Microsoft.AspNetCore.Authorization.IAuthorizationService _AuthorizationService;

        #endregion

        #region Constructor
        public UserHandler(UserManager<User> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService, Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService)
        {
            _UserManager = userManager;
            _Mapper = mapper;
            _HttpContextAccessor = httpContextAccessor;
            _UserService = userService;
            _UserService = userService;
            _AuthorizationService = authorizationService;
        }

        #endregion

        #region handler

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var UserMapping = _Mapper.Map<User>(request);

            var createResult = await _UserService.AddUserAsync(UserMapping, request.Password);
            switch (createResult)
            {
                case "EmailIsExist": return ResponseHandler.BadRequest<string>("EmailIsExist");
                case "UserNameIsExist": return ResponseHandler.BadRequest<string>("UserNameIsExist");
                case "ErrorInCreateUser": return ResponseHandler.BadRequest<string>("FaildToAddUser");
                case "Failed": return ResponseHandler.BadRequest<string>("TryToRegisterAgain");
                case "Success": return ResponseHandler.Success<string>("");
                default: return ResponseHandler.BadRequest<string>(createResult);
            }



        }
        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {

            var userResult = await _UserManager.FindByIdAsync(request.Id.ToString());

            if (userResult == null)
            {
                return ResponseHandler.NotFound<string>("User Not Found");
            }

            //ownership
            var authorizationResult = await _AuthorizationService.AuthorizeAsync(_HttpContextAccessor.HttpContext!.User, request.Id, "OwnerPolicy");

            if (!authorizationResult.Succeeded)
            {
                return ResponseHandler.Forbidden<string>();
            }

            //if username is Exist
            var userByUserName = await _UserManager.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName && x.Id != request.Id);
            //username is Exist
            if (userByUserName != null)
            {
                return ResponseHandler.BadRequest<string>("UserNameIsExist");
            }

            var UserMapper = _Mapper.Map(request, userResult);

            var result = await _UserManager.UpdateAsync(UserMapper);

            if (result.Succeeded)
            {
                return ResponseHandler.Success("Edited Successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>(result.Errors.FirstOrDefault().Description);
            }
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _UserManager.FindByIdAsync(request.Id.ToString());

            if (userResult == null)
            {
                return ResponseHandler.NotFound<string>("User Not Found");
            }

            var result = await _UserManager.DeleteAsync(userResult);

            if (result.Succeeded)
            {
                return ResponseHandler.Success("Deleted Successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>(result.Errors.FirstOrDefault().Description);

            }
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //if user exist
            var userResult = await _UserManager.FindByIdAsync(request.Id.ToString());

            //ownership
            var authorizationResult = await _AuthorizationService.AuthorizeAsync(_HttpContextAccessor.HttpContext!.User, request.Id, "OwnerPolicy");

            if (!authorizationResult.Succeeded)
            {
                return ResponseHandler.Forbidden<string>();
            }

            if (userResult == null)
            {
                return ResponseHandler.NotFound<string>("User Not Found");
            }

            var result = await _UserManager.ChangePasswordAsync(userResult, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded)
            {
                return ResponseHandler.Success("Deleted Successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>(result.Errors.FirstOrDefault().Description);

            }
        }


        #endregion
    }
}
