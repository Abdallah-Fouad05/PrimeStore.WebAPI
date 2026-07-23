using MediatR;
using Microsoft.AspNetCore.Http;
using PrimeStore.core.Features.Cart.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Cart.Commands.Handlers
{
    public class CartHandler : IRequestHandler<AddCartItemCommand, Response<string>>,
                               IRequestHandler<UpdateCartItemCommand, Response<string>>,
                               IRequestHandler<DeleteCartItemCommand, Response<string>>


    {

        #region  Fields
        private readonly ICartService _CartService;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly Microsoft.AspNetCore.Authorization.IAuthorizationService _AuthorizationService;
        #endregion

        #region Constructor
        public CartHandler(ICartService cartService, IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService)
        {
            _CartService = cartService;
            _HttpContextAccessor = httpContextAccessor;
            _AuthorizationService = authorizationService;
        }
        #endregion

        public async Task<Response<string>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var result = await _AuthorizationService.AuthorizeAsync(_HttpContextAccessor.HttpContext.User, request.UserId, "OwnerPolicy");
            if (!result.Succeeded)
            {
                return ResponseHandler.Forbidden<string>();
            }

            var response = await _CartService.AddCartItemAsync(request);
            switch (response)
            {
                case "User Cart Not Found":
                    return ResponseHandler.BadRequest<string>(response);

                case "Product Not Found":
                    return ResponseHandler.BadRequest<string>(response);

                case "Cart Quantity greater than Product Stock":
                    return ResponseHandler.BadRequest<string>(response);

                case "Product Stock Is Empty":
                    return ResponseHandler.BadRequest<string>(response);

                case ResultString.Success:
                    return ResponseHandler.Success<string>(response);

                default:
                    return ResponseHandler.BadRequest<string>();
            }
        }

        public async Task<Response<string>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var result = await _AuthorizationService.AuthorizeAsync(_HttpContextAccessor.HttpContext.User, request.UserId, "OwnerPolicy");
            if (!result.Succeeded)
            {
                return ResponseHandler.Forbidden<string>();
            }

            var response = await _CartService.UpdateCartItemAsync(request);
            switch (response)
            {
                case "Cart Item Not Found":
                    return ResponseHandler.BadRequest<string>(response);

                case "Product Not Found":
                    return ResponseHandler.BadRequest<string>(response);

                case "Cart Quantity greater than Product Stock":
                    return ResponseHandler.BadRequest<string>(response);

                case "Product Stock Is Empty":
                    return ResponseHandler.BadRequest<string>(response);

                case ResultString.Success:
                    return ResponseHandler.Success<string>(response);

                default:
                    return ResponseHandler.BadRequest<string>();
            }
        }
        public async Task<Response<string>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var result = await _AuthorizationService.AuthorizeAsync(_HttpContextAccessor.HttpContext.User, request.UserId, "OwnerPolicy");
            if (!result.Succeeded)
            {
                return ResponseHandler.Forbidden<string>();
            }

            var response = await _CartService.DeleteCartItemAsync(request.CartItemId);
            switch (response)
            {
                case "Cart Item Not Found":
                    return ResponseHandler.BadRequest<string>(response);
                case ResultString.Success:
                    return ResponseHandler.Success<string>(response);
                default:
                    return ResponseHandler.BadRequest<string>();
            }
        }
    }
}
