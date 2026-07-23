using MediatR;
using PrimeStore.core.Features.Users.Queries.Results;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Users.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int UserId { get; set; }

        public GetUserByIdQuery(int id)
        {
            UserId = id;
        }
    }
}
