using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Products.Commands.Models
{
    //request
    public class DeleteProductCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }

    }
}
