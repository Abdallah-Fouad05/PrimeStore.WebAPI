using PrimeStore.data.Entities;

namespace PrimeStore.service.Abstracts
{
    public interface IWishlistService
    {
        IQueryable<Wishlist> GetUserWishlistQueryable(int UserId);
        Task<Wishlist> GetByIdAsync(int Id);
        Task<string> AddProductToWishList(Wishlist wishlist);
        Task<string> RemoveProductFromWishList(Wishlist wishlist);

        Task<bool> HasUserWishlistedProductAsync(int userId, int productId);
    }
}
