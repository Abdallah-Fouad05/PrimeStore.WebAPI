using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Status;

namespace PrimeStore.core.Features.Brands.Commands.Models
{
    public class AddBrandCommand : IRequest<Response<string>>
    {
        public string BrandName { get; set; }
        public string? ImageUrl { get; set; }
        public GenericStatusEnum BrandStatus { get; set; }

        public AddBrandCommand() { }

        public AddBrandCommand(string brandName, string? imageUrl, GenericStatusEnum status)
        {
            BrandName = brandName;
            ImageUrl = imageUrl;
            BrandStatus = status;
        }
    }
}
