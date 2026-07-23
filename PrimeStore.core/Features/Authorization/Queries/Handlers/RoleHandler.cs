using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Authorization.Queries.Models;
using PrimeStore.core.Features.Authorization.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.data.Results;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Authorization.Queries.Handlers
{
    public class RoleHandler : IRequestHandler<GetRolesQuery, Response<List<GetRolesResponse>>>,
                               IRequestHandler<GetRoleByIdQuery, Response<GetRolesResponse>>,
                               IRequestHandler<ManageUserRoleQuery, Response<ManageUserRoleResult>>
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

        #endregion

        #region handler
        public async Task<Response<List<GetRolesResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var Roles = await _AuthorizationService.GetRolesListAsync();

            var RolesMapping = _Mapper.Map<List<GetRolesResponse>>(Roles);

            return ResponseHandler.Success(RolesMapping);
        }

        public async Task<Response<GetRolesResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var Role = await _AuthorizationService.GetRoleByIdAsync(request.Id);

            if (Role == null)
            {
                return ResponseHandler.NotFound<GetRolesResponse>("Role Not Found");
            }

            var RolesMapping = _Mapper.Map<GetRolesResponse>(Role);

            return ResponseHandler.Success(RolesMapping);
        }

        public async Task<Response<ManageUserRoleResult>> Handle(ManageUserRoleQuery request, CancellationToken cancellationToken)
        {
            var UserRoles = await _AuthorizationService.GetUserRoles(request.UserId);

            if (UserRoles == null)
            {
                return ResponseHandler.NotFound<ManageUserRoleResult>("User Not Found");
            }

            return ResponseHandler.Success(UserRoles);
        }






        #endregion
    }
}
