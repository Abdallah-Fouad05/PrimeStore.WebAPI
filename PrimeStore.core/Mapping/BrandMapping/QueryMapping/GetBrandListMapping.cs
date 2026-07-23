using PrimeStore.core.Features.Brands.Queries.Results;
using PrimeStore.data.Entities;

namespace PrimeStore.core.Mapping.BrandMapping
{
    public partial class BrandMapping
    {
        public void GetBrandListMapping()
        {
            CreateMap<Brand, GetBrandListResponse>()
                .ForPath(
                    dest => dest.BrandId,
                    opt => opt.MapFrom(src => src.BrandId))
                .ForPath(
                    dest => dest.BrandName,
                    opt => opt.MapFrom(src => src.BrandName))
                .ForPath(
                    dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.ImageUrl))
                 .ForPath(dest => dest.BrandStatus,
                 otp => otp.MapFrom(src => new StatusResponse { StatusId = src.Status.StatusId, StatusName = src.Status.StatusName }));

        }
    }
}
