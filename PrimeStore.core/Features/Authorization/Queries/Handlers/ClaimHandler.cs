using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using MediatR;
using PrimeStore.core.Features.Authorization.Queries.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Results;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Authorization.Queries.Handlers
{
    public class ClaimHandler : IRequestHandler<ManageUserClaimQuery,Core.Bases.Response<ManageUserClaimResult>>
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

        public async Task<Core.Bases.Response<ManageUserClaimResult>> Handle(ManageUserClaimQuery request, CancellationToken cancellationToken)
        {
            var UserClaims = await _AuthorizationService.GetUserClaims(request.UserId);

            if (UserClaims == null)
            {
                return ResponseHandler.NotFound<ManageUserClaimResult>("User Not Found");
            }

            return ResponseHandler.Success(UserClaims);
        }



        #endregion
    }
}
