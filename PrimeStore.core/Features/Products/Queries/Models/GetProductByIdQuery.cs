using MediatR;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Products.Queries.Models
{
    public class GetProductByIdQuery : IRequest<Response<GetProductByIdResponse>>
    {
        public int ProductId { get; set; }

        public GetProductByIdQuery(int Id)
        {
            ProductId = Id;
        }
    }
}
