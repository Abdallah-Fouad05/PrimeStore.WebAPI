using MediatR;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.Core.Wrappers;

namespace PrimeStore.core.Features.Products.Queries.Models
{
    public class GetProductPaginatedListByBrandIdQuery : IRequest<PaginatedResult<GetProductListResponse>>
    {
        public int BrandId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
