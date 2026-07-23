using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PrimeStore.core.Features.Cart.Queries.Models;
using PrimeStore.core.Features.Cart.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Cart.Queries.Handlers
{
    public class CartHandler : IRequestHandler<GetUserCartItemsListQuery, Response<List<GetUserCartItemsListResponse>>>
    {

        #region  Fields
        private readonly ICartService _CartService;
        private readonly IMapper _Mapper;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly Microsoft.AspNetCore.Authorization.IAuthorizationService _AuthorizationService;
        #endregion

        #region Constructor
        public CartHandler(ICartService cartService, IMapper mapper, Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _CartService = cartService;
            _Mapper = mapper;
            _AuthorizationService = authorizationService;
            _HttpContextAccessor = httpContextAccessor;
        }
        #endregion
        public async Task<Response<List<GetUserCartItemsListResponse>>> Handle(GetUserCartItemsListQuery request, CancellationToken cancellationToken)
        {
            var result = await _AuthorizationService.AuthorizeAsync(_HttpContextAccessor.HttpContext.User, request.UserId, "OwnerPolicy");
            if (!result.Succeeded)
            {
                return ResponseHandler.Forbidden<List<GetUserCartItemsListResponse>>();
            }

            var CartItems = await _CartService.GetUserCartItemsAsync(request.UserId);

            var CartItemsMapping = _Mapper.Map<List<GetUserCartItemsListResponse>>(CartItems);

            return ResponseHandler.Success(CartItemsMapping);
        }
    }
}
