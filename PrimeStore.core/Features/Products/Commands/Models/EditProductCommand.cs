using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Status;

namespace PrimeStore.core.Features.Products.Commands.Models
{
    //request
    public class EditProductCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public GenericStatusEnum ProductStatus { get; set; }
        public List<ProductAttributeRequest> ProductAttribute { get; set; }
        public List<ProductImageRequest> ProductImage { get; set; }

    }
}
