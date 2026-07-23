using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities;
using PrimeStore.data.Helper;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.service.Abstracts;

namespace PrimeStore.service.Implementations
{
    public class WishlistService : IWishlistService
    {
        #region Feilds
        private readonly IWishlistRepository _WishlistRepository;
        #endregion

        #region Constructor
        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _WishlistRepository = wishlistRepository;
        }
        #endregion

        #region Service
        public IQueryable<Wishlist> GetUserWishlistQueryable(int UserId)
        {
            return _WishlistRepository
                .GetTableNoTracking()
                .Include(x => x.Product)
                    .ThenInclude(x => x.Brand)
                .Include(x => x.Product)
                    .ThenInclude(x => x.Category)
                        .ThenInclude(x => x.ParentCategory)
                .Include(x => x.Product)
                    .ThenInclude(x => x.Status)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ProductImages)
                .Include(x => x.Product)
                    .ThenInclude(x => x.ProductAttributes)
                .AsQueryable();
        }
        public async Task<string> AddProductToWishList(Wishlist wishlist)
        {
            wishlist.CreatedAt = DateTime.UtcNow;
            await _WishlistRepository.AddAsync(wishlist);
            return ResultString.Success;
        }
        public async Task<string> RemoveProductFromWishList(Wishlist wishlist)
        {
            var trans = _WishlistRepository.BeginTransaction();
            try
            {
                await _WishlistRepository.DeleteAsync(wishlist);
                trans.Commit();
                return ResultString.Success;
            }
            catch
            {
                trans.Rollback();
                return ResultString.Failure;
            }
        }

        public async Task<Wishlist> GetByIdAsync(int Id)
        {
            return await _WishlistRepository.GetByIdAsync(Id);
        }

        public async Task<bool> HasUserWishlistedProductAsync(int userId, int productId)
        {
            var result = await _WishlistRepository.GetTableAsTracking().FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
