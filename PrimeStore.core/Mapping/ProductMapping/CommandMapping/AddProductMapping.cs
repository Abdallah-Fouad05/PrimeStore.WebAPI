using PrimeStore.core.Features.Products.Commands.Models;
using PrimeStore.data.Entities;

namespace PrimeStore.core.Mapping.ProductMapping
{
    public partial class ProductProfile
    {
        public void AddProductMapping()
        {

            CreateMap<AddProductCommand, Product>()
                .ForPath(
                    dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.CategoryId)
                ).ForPath(
                    dest => dest.BrandId,
                    opt => opt.MapFrom(src => src.BrandId)
                ).ForPath(
                    dest => dest.StatusId,
                    opt => opt.MapFrom(src => (int)src.ProductStatus)
                ).ForPath(
                    dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImage.Select(x => new ProductImage { ProductImageUrl = x.ImageUrl, position = x.Position, IsCover = x.IsCover }))
                ).ForPath(
                    dest => dest.ProductAttributes,
                    opt => opt.MapFrom(src => src.ProductAttribute.Select(x => new ProductAttribute { Key = x.Key, Value = x.Value })));
        }
    }
}
