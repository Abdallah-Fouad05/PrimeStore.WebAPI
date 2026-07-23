using MediatR;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.Core.Wrappers;

namespace PrimeStore.core.Features.Products.Queries.Models
{
    public class GetProductPaginatedListByCategoryIdQuery : IRequest<PaginatedResult<GetProductListResponse>>
    {
        public int CategoryId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
