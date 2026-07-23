using MediatR;
using PrimeStore.core.Features.Reviews.Queries.Results;
using PrimeStore.Core.Wrappers;

namespace PrimeStore.core.Features.Reviews.Queries.Models
{
    public class GetUserReviewsQuery : IRequest<PaginatedResult<GetUserReviewsResponse>>
    {
        public int UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetUserReviewsQuery(int userId, int pageNumber, int pageSize)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
