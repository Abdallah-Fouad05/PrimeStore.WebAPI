using MediatR;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.Core.Wrappers;
using PrimeStore.data.Helper;

namespace PrimeStore.core.Features.Products.Queries.Models
{
    public class GetActiveProductPaginatedListQuery : IRequest<PaginatedResult<GetProductListResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public ProductOrderingEnum OrderBy { get; set; }

        public string? Search { get; set; }

        public GetActiveProductPaginatedListQuery(int pageNumber, int pageSize, ProductOrderingEnum orderby, string search)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            OrderBy = orderby;
            Search = search;
        }

        public GetActiveProductPaginatedListQuery()
        {

        }

    }
}
