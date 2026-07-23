using MediatR;

namespace PrimeStore.core.Features.Wishlist.Queries.Models
{
    public class IsProductInUserWishlistQuery : IRequest<bool>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
