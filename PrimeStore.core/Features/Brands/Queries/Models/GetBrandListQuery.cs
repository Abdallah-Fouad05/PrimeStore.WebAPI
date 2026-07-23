using MediatR;
using PrimeStore.core.Features.Brands.Queries.Results;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Brands.Queries.Models
{
    public class GetBrandListQuery : IRequest<Response<List<GetBrandListResponse>>>
    {
    }
}
