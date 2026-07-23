using MediatR;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper.Status;

namespace PrimeStore.core.Features.Products.Commands.Models
{
    public class ProductImageRequest
    {
        public string ImageUrl
        {
            get; set;
        }

        public int Position { get; set; }
        public bool IsCover { get; set; }
    }
    public class ProductAttributeRequest
    {

        public string Key { get; set; }
        public string Value { get; set; }
    }
    //request
    public class AddProductCommand : IRequest<Response<string>>
    {
        public required string Title { get; set; }
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
