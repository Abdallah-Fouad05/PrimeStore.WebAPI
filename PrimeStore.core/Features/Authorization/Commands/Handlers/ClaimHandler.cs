using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PrimeStore.Core.Bases;
using PrimeStore.core.Features.Authorization.Commands.Models;
using PrimeStore.data.Helper;
using MediatR;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Authorization.Commands.Handlers
{
    public class ClaimHandler : IRequestHandler<UpdateUserClaimsCommand,Response<string>>
    {
        #region Fields
        private readonly IAuthorizationService _AuthorizationService;
        private readonly IMapper _Mapper;
        #endregion

        #region Constructor
        public ClaimHandler(IAuthorizationService authorizationService, IMapper mapper)
        {
            _AuthorizationService = authorizationService;
            _Mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            string response = await _AuthorizationService.UpdateUserClaims(request);

            switch (response)
            {
                case "UserNotFound":
                    return ResponseHandler.NotFound<string>("User Not Found");

                case "FailedToRemoveOldClaims":
                    return ResponseHandler.BadRequest<string>("Failed To Remove Old Roles");

                case "FailedToAddNewClaims":
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
