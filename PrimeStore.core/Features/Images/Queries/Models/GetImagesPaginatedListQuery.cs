using MediatR;
using PrimeStore.core.Features.Images.Queries.Results;
using PrimeStore.Core.Wrappers;

namespace PrimeStore.core.Features.Images.Queries.Models
{
    public class GetImagesPaginatedListQuery : IRequest<PaginatedResult<GetImagesPaginatedListResponse>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
