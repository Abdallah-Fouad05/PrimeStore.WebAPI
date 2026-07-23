using AutoMapper;
using PrimeStore.core.Features.Cart.Queries.Results;
using PrimeStore.data.Entities;

public class CartMapping : Profile
{
    public CartMapping()
    {
        CreateMap<CartItem, GetUserCartItemsListResponse>()
            .ForMember(dest => dest.Product,
                opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.Total,
                opt => opt.MapFrom(src => src.Total));

        CreateMap<Product, CartItemProduct>()
            .ForMember(dest => dest.ProductId,
                opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.ProductDescription,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Price))
             .ForMember(dest => dest.ProductImage,
                opt => opt.MapFrom(src =>
                    src.ProductImages
                        .Where(x => x.IsCover)
                        .Select(x => x.ProductImageUrl)
                        .FirstOrDefault()))
            .ForMember(dest => dest.Brand,
                opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.Category));

        CreateMap<Brand, ProductBrand>()
            .ForMember(dest => dest.BrandId,
                opt => opt.MapFrom(src => src.BrandId))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.BrandName));

        CreateMap<Category, ProductCategory>()
            .ForMember(dest => dest.CategoryId,
                opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.CategoryName));
    }
}