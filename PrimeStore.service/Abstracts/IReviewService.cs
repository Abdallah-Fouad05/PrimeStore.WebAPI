using PrimeStore.data.Entities;

namespace PrimeStore.service.Abstracts
{
    public interface IReviewService
    {
        IQueryable<Review> GetProductReviewsQueryable(int ProductId);
        IQueryable<Review> GetUserReviewsQueryable(int UserId);
        Task<Review?> GetReviewByIdAsync(int reviewId);
        Task<string> AddProductReview(Review review);
        Task<string> UpdateProductReview(Review review);
        Task<string> DeleteProductReview(Review review);
        Task<bool> IsReviewExist(int UserId, int ProductId);
    }
}
