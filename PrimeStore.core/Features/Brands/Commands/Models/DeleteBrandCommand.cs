using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Brands.Commands.Models
{
    public class DeleteBrandCommand : IRequest<Response<string>>
    {
        public int BrandId { get; set; }

        public DeleteBrandCommand(int brandId)
        {
            BrandId = brandId;
        }
    }
}
