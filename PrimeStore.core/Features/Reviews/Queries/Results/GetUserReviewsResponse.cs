using PrimeStore.core.Features.Products.Queries.Results;

namespace PrimeStore.core.Features.Reviews.Queries.Results
{

    public class GetUserReviewsResponse
    {
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public float Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public GetProductListResponse Product { get; set; }

        public GetUserReviewsResponse(int reviewId, string comment, float rating, DateTime? createdAt, DateTime? updatedAt, GetProductListResponse product)
        {
            ReviewId = reviewId;
            Comment = comment;
            Rating = rating;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Product = product;
        }
    }
}
