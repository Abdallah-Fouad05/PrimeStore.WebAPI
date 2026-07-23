using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Wishlist.Commands.Models
{
    public class DeleteWishlistCommand : IRequest<Response<string>>
    {
        public int WishlistId { get; set; }

        public DeleteWishlistCommand(int wishlistId)
        {
            WishlistId = wishlistId;
        }
    }
}
