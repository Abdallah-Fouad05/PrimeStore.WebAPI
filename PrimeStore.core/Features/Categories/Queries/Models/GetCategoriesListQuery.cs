using MediatR;
using PrimeStore.core.Features.Categories.Queries.Results;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Categories.Queries.Models
{
    public class GetCategoriesListQuery : IRequest<Response<List<GetCategoriesListResponse>>>
    {

    }
}
