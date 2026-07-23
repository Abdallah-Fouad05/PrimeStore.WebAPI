using MediatR;

namespace PrimeStore.core.Features.Cart.Commands.Models
{
    public class DeleteCartItemCommand : IRequest<Core.Bases.Response<string>>
    {
        public int CartItemId { get; set; }

        public int UserId { get; set; }

        public DeleteCartItemCommand(int cartItemId, int userId)
        {
            CartItemId = cartItemId;
            UserId = userId;
        }
    }
}
