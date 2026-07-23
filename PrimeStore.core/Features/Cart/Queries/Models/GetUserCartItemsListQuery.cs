using MediatR;
using PrimeStore.core.Features.Cart.Queries.Results;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Cart.Queries.Models
{
    public class GetUserCartItemsListQuery : IRequest<Response<List<GetUserCartItemsListResponse>>>
    {
        public int UserId { get; set; }

        public GetUserCartItemsListQuery(int userid)
        {
            UserId = userid;
        }
    }
}
