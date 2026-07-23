using PrimeStore.core.Features.Products.Commands.Models;
using PrimeStore.data.Entities;

namespace PrimeStore.core.Mapping.ProductMapping
{
    public partial class ProductProfile
    {
        public void EditProductMapping()
        {

            CreateMap<EditProductCommand, Product>()
                .ForPath(
                    dest => dest.ProductId,
                    opt => opt.MapFrom(src => src.ProductId)
                ).ForPath(
                    dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.CategoryId)
                ).ForPath(
                    dest => dest.BrandId,
                    opt => opt.MapFrom(src => src.BrandId)
                ).ForPath(
                    dest => dest.StatusId,
                    otp => otp.MapFrom(src => (int)src.ProductStatus)
                ).ForPath(
                    dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImage.Select(x => new ProductImage { ProductId = src.ProductId, ProductImageUrl = x.ImageUrl, position = x.Position, IsCover = x.IsCover }))
                ).ForPath(
                    dest => dest.ProductAttributes,
                    opt => opt.MapFrom(src => src.ProductAttribute.Select(x => new ProductAttribute { ProductId = src.ProductId, Key = x.Key, Value = x.Value })));
        }
    }
}
