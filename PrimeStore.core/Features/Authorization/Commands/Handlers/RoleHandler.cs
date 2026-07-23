using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Authorization.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Authorization.Commands.Handlers
{
    public class RoleHandler : IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {
        #region Fields
        private readonly IAuthorizationService _AuthorizationService;
        private readonly IMapper _Mapper;
        #endregion

        #region Constructor
        public RoleHandler(IAuthorizationService authorizationService, IMapper mapper)
        {
            _AuthorizationService = authorizationService;
            _Mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            string response = await _AuthorizationService.UpdateUserRoles(request);

            switch (response)
            {
                case "UserNotFound":
                    return ResponseHandler.NotFound<string>("User Not Found");

                case "FailedToRemoveOldRoles":
                    return ResponseHandler.BadRequest<string>("Failed To Remove Old Roles");

                case "FailedToAddNewRoles":
                    return ResponseHandler.BadRequest<string>("Failed To Add New Roles");

                case ResultString.Success:
                    return ResponseHandler.Success("User roles updated successfully");

                case ResultString.Failure:
                    return ResponseHandler.BadRequest<string>("Failed to update user roles");

                default:
                    return ResponseHandler.BadRequest<string>("An unexpected error occurred");
            }
        }

        #endregion
    }
}
