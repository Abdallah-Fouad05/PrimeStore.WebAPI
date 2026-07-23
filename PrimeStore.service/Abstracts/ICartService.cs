using PrimeStore.data.Entities;
using PrimeStore.data.Results;

namespace PrimeStore.service.Abstracts
{
    public interface ICartService
    {
        Task<string> AddCartAsync(Cart cart);
        Task<string> AddCartItemAsync(AddCartItemResult cartItem);
        Task<string> UpdateCartItemAsync(UpdateCartItemResult cartItem);
        Task<string> DeleteCartItemAsync(int CartItemId);
        Task<ICollection<CartItem>> GetUserCartItemsAsync(int UserId);
    }
}
