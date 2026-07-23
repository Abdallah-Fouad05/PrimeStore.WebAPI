using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Wishlist.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Wishlist.Commands.Handlers
{
    internal class Wishlisthandler : IRequestHandler<AddWishlistCommand, Response<string>>,
                                     IRequestHandler<DeleteWishlistCommand, Response<string>>
    {
        #region Fields
        private readonly IWishlistService _WishlistService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public Wishlisthandler(IWishlistService wishlistService, IMapper mapper)
        {
            _WishlistService = wishlistService;
            _mapper = mapper;
        }

        #endregion

        #region Handler
        public async Task<Response<string>> Handle(AddWishlistCommand request, CancellationToken cancellationToken)
        {
            var wishlist = _mapper.Map<PrimeStore.data.Entities.Wishlist>(request);

            var result = await _WishlistService.AddProductToWishList(wishlist);

            if (result == ResultString.Success)
            {
                return ResponseHandler.Success("Product Added Successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>("Failed to Add Product");
            }
        }
        public async Task<Response<string>> Handle(DeleteWishlistCommand request, CancellationToken cancellationToken)
        {
            var wishlistResult = await _WishlistService.GetByIdAsync(request.WishlistId);

            if (wishlistResult == null)
                return ResponseHandler.NotFound<string>("Wishlist  not found");

            var wishlistMapping = _mapper.Map(request, wishlistResult);

            var result = await _WishlistService.RemoveProductFromWishList(wishlistMapping);

            if (result == ResultString.Success)
            {
                return ResponseHandler.Success("Deleted successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>("Failed to Deleted");
            }
        }
        #endregion
    }
}
