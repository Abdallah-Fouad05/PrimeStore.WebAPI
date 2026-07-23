using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Status;

namespace PrimeStore.core.Features.Brands.Commands.Models
{
    public class EditBrandCommand : IRequest<Response<string>>
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string? ImageUrl { get; set; }
        public GenericStatusEnum BrandStatus { get; set; }

        public EditBrandCommand() { }

        public EditBrandCommand(int brandId, string brandName, string? imageUrl, GenericStatusEnum status)
        {
            BrandId = brandId;
            BrandName = brandName;
            ImageUrl = imageUrl;
            BrandStatus = status;
        }
    }
}
