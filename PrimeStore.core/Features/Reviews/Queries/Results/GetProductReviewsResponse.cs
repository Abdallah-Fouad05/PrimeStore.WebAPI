namespace PrimeStore.core.Features.Reviews.Queries.Results
{
    public class UserReviewResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class GetProductReviewsResponse
    {
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public float Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserReviewResponse User { get; set; }

        public GetProductReviewsResponse(int reviewId, string comment, float rating, DateTime? createdAt, DateTime? updatedAt, UserReviewResponse user)
        {
            ReviewId = reviewId;
            Comment = comment;
            Rating = rating;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            User = user;
        }
    }
}
