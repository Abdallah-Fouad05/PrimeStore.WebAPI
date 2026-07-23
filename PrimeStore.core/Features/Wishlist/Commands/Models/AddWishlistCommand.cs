using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Wishlist.Commands.Models
{
    public class AddWishlistCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
