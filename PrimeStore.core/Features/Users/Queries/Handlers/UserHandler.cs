using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrimeStore.core.Features.Users.Queries.Models;
using PrimeStore.core.Features.Users.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.Core.Wrappers;
using PrimeStore.data.Entities.Identity;

namespace PrimeStore.core.Features.Users.Queries.Handlers
{
    public class UserHandler : IRequestHandler<GetUserPaginatedListQuery, PaginatedResult<GetUserPaginatedListResponse>>,
                              IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        #region Fields
        private readonly UserManager<User> _UserManager;
        private readonly IMapper _Mapper;
        private readonly Microsoft.AspNetCore.Authorization.IAuthorizationService _AuthorizationService;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        #endregion

        #region Constructor
        public UserHandler(UserManager<User> userManager, IMapper mapper, Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _UserManager = userManager;
            _Mapper = mapper;
            _AuthorizationService = authorizationService;
            _HttpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Handler
        public async Task<PaginatedResult<GetUserPaginatedListResponse>> Handle(GetUserPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var Queryable = _UserManager.Users.Include(x => x.UserStatus).AsQueryable();
            var PaginatedList = await _Mapper.ProjectTo<GetUserPaginatedListResponse>(Queryable).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            return PaginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _UserManager.Users.Include(x => x.UserStatus).FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
                return ResponseHandler.NotFound<GetUserByIdResponse>("User Not Found");

            var response = await _AuthorizationService.AuthorizeAsync(_HttpContextAccessor.HttpContext!.User, request.UserId, "OwnerPolicy");

            if (!response.Succeeded)
            {
                return ResponseHandler.Forbidden<GetUserByIdResponse>();
            }

            var UserMapping = _Mapper.Map<GetUserByIdResponse>(user);

            return ResponseHandler.Success(UserMapping);
        }
        #endregion
    }
}
