using MediatR;
using PrimeStore.core.Features.Wishlist.Queries.Results;
using PrimeStore.Core.Wrappers;

namespace PrimeStore.core.Features.Wishlist.Queries.Models
{
    public class GetUserWishlistpaginatedListQuery : IRequest<PaginatedResult<GetUserwishlistPaginatedListResponse>>
    {
        public int UserId { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public GetUserWishlistpaginatedListQuery(int userId, int pageNumber, int pageSize)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
