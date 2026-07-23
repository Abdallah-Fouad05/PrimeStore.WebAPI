using MediatR;
using PrimeStore.core.Features.Authorization.Queries.Results;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRolesResponse>>
    {
        public int Id { get; set; }

        public GetRoleByIdQuery(int id)
        {
            Id = id;
        }
    }
}
