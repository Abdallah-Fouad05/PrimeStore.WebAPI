using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities;
using PrimeStore.data.Helper;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.service.Abstracts;

namespace PrimeStore.service.Implementations
{
    public class ReviewService : IReviewService
    {
        #region Fields
        private readonly IReviewRepository _ReviewRepository;
        #endregion

        #region Constructor
        public ReviewService(IReviewRepository reviewRepository)
        {
            _ReviewRepository = reviewRepository;
        }

        #endregion

        #region Service

        public IQueryable<Review> GetProductReviewsQueryable(int id)
        {
            return _ReviewRepository.GetTableNoTracking().Include(x => x.User).Where(x => x.ProductId == id).AsQueryable();
        }
        public IQueryable<Review> GetUserReviewsQueryable(int id)
        {
            return _ReviewRepository.GetTableNoTracking()
                .Include(x => x.Product).ThenInclude(x => x.Brand)
                .Include(x => x.Product).ThenInclude(x => x.Category).ThenInclude(x => x.ParentCategory)
                .Include(x => x.Product).ThenInclude(x => x.ProductImages)
                .Include(x => x.Product).ThenInclude(x => x.ProductAttributes)
                .Include(x => x.Product).ThenInclude(x => x.Status)
                .Where(x => x.UserId == id).AsQueryable();
        }
        public async Task<string> AddProductReview(Review review)
        {
            review.CreatedAt = DateTime.UtcNow;
            await _ReviewRepository.AddAsync(review);
            return ResultString.Success;
        }
        public async Task<string> UpdateProductReview(Review review)
        {
            review.UpdatedAt = DateTime.UtcNow;
            await _ReviewRepository.UpdateAsync(review);
            return ResultString.Success;
        }
        public async Task<string> DeleteProductReview(Review review)
        {
            var trans = _ReviewRepository.BeginTransaction();
            try
            {
                await _ReviewRepository.DeleteAsync(review);
                trans.Commit();
                return ResultString.Success;
            }
            catch
            {
                trans.Rollback();
                return ResultString.Failure;
            }
        }

        public async Task<Review?> GetReviewByIdAsync(int reviewId)
        {
            return await _ReviewRepository.GetTableNoTracking().Include(x => x.User).FirstOrDefaultAsync(x => x.ReviewId == reviewId);
        }

        public async Task<bool> IsReviewExist(int UserId, int ProductId)
        {
            var review = await _ReviewRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.UserId == UserId && x.ProductId == ProductId);

            if (review == null)
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}
