using PrimeStore.core.Features.Brands.Queries.Results;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.data.Entities;

namespace PrimeStore.core.Mapping.ProductMapping
{
    public partial class ProductProfile
    {
        public void GetProductByIdMapping()
        {
            CreateMap<Product, GetProductByIdResponse>()
                .ForPath(
                    dest => dest.Category.CategoryId,
                    opt => opt.MapFrom(src => src.Category.CategoryId)
                )
                .ForPath(
                    dest => dest.Category.CategoryName,
                    opt => opt.MapFrom(src => src.Category.CategoryName)
                )
                .ForPath(
                    dest => dest.Category.ImageUrl,
                    opt => opt.MapFrom(src => src.Category.ImageUrl)
                ).ForPath(
                    dest => dest.Brand.BrandId,
                    opt => opt.MapFrom(src => src.Brand.BrandId)
                )
                .ForPath(
                    dest => dest.Brand.BrandName,
                    opt => opt.MapFrom(src => src.Brand.BrandName)
                )
                .ForPath(
                    dest => dest.Brand.ImageUrl,
                    opt => opt.MapFrom(src => src.Brand.ImageUrl)
                ).ForPath(
                    dest => dest.Category.ParentCategory.CategoryId,
                    opt => opt.MapFrom(src => src.Category.ParentCategory.CategoryId)
                )
                .ForPath(
                    dest => dest.Category.ParentCategory.CategoryName,
                    opt => opt.MapFrom(src => src.Category.ParentCategory.CategoryName)
                )
                .ForPath(
                    dest => dest.Category.ParentCategory.ImageUrl,
                    opt => opt.MapFrom(src => src.Category.ParentCategory.ImageUrl)
                )
                .ForPath(
                dest => dest.Images,
                otp => otp.MapFrom(src => src.ProductImages)
                )
                .ForPath(
                    dest => dest.Reviews,
                    otp => otp.MapFrom(src => src.Reviews.Select(r => new ReviewResponse
                    {
                        ReviewId = r.ReviewId,
                        Comment = r.Comment,
                        Rating = r.Rating,
                        CreatedAt = r.CreatedAt,
                        UpdatedAt = r.UpdatedAt,
                        User = new UserResponse { UserId = r.User.Id, UserName = r.User.UserName, ImageUrl = r.User.ImageUrl }
                    }
                    ))
                ).ForPath(
                dest => dest.ProductStatus,
                opt => opt.MapFrom(src => new StatusResponse { StatusId = src.Status.StatusId, StatusName = src.Status.StatusName })
                ).ForPath(
                dest => dest.Images,
                opt => opt.MapFrom(src => src.ProductImages.Select(x => new ProductImageResponse { ImageId = x.ProductImageId, ImageUrl = x.ProductImageUrl, IsCover = x.IsCover, Position = x.position }))
                ).ForPath(
                dest => dest.Attributes,
                opt => opt.MapFrom(src => src.ProductAttributes.Select(x => new ProductAttributeResponse { ProductAttributeId = x.ProductAttributeId, Key = x.Key, Value = x.Value })));
        }
    }
}
