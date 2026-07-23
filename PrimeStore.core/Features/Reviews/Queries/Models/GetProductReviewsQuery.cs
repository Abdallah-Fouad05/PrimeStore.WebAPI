using MediatR;
using PrimeStore.core.Features.Reviews.Queries.Results;
using PrimeStore.Core.Wrappers;

namespace PrimeStore.core.Features.Reviews.Queries.Models
{
    public class GetProductReviewsQuery : IRequest<PaginatedResult<GetProductReviewsResponse>>
    {
        public int ProductId { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public GetProductReviewsQuery(int productId, int pageNumber, int pageSize)
        {
            ProductId = productId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
