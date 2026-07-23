using MediatR;
using PrimeStore.core.Features.Authorization.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Authorization.Commands.Handlers
{
    public class StatusHandler : IRequestHandler<UpdateUserStatusCommand, Response<string>>
    {
        #region Fields
        private readonly IUserService _UserService;
        #endregion

        #region Constructor
        public StatusHandler(IUserService userService)
        {
            _UserService = userService;
        }

        public async Task<Response<string>> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            string response = await _UserService.UpdateUserStatus(request.UserId, (int)request.UserStatus);

            switch (response)
            {
                case ResultString.NotFound:
                    return ResponseHandler.NotFound<string>("User Not Found");

                case ResultString.Success:
                    return ResponseHandler.Success("User Status updated successfully");

                case ResultString.Failure:
                    return ResponseHandler.BadRequest<string>("Failed to update user Status");

                default:
                    return ResponseHandler.BadRequest<string>("An unexpected error occurred");
            }
        }

        #endregion
    }
}
