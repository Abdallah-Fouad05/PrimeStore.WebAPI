using MediatR;
using PrimeStore.core.Features.Users.Queries.Results;
using PrimeStore.Core.Wrappers;

namespace PrimeStore.core.Features.Users.Queries.Models
{
    public class GetUserPaginatedListQuery : IRequest<PaginatedResult<GetUserPaginatedListResponse>>
    {
        public int PageNumber { get; set; }  = 1;
        public int PageSize { get; set; } = 10;
    }
}
