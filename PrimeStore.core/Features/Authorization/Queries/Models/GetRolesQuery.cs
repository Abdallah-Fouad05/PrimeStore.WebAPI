using MediatR;
using PrimeStore.core.Features.Authorization.Queries.Results;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Authorization.Queries.Models
{
    public class GetRolesQuery : IRequest<Response<List<GetRolesResponse>>>
    {

    }
}
