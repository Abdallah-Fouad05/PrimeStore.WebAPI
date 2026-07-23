using PrimeStore.core.Features.Brands.Commands.Models;
using PrimeStore.data.Entities;

namespace PrimeStore.core.Mapping.BrandMapping
{
    public partial class BrandMapping
    {
        public void EditBrandMapping()
        {
            CreateMap<EditBrandCommand, Brand>()
                .ForPath(dest => dest.BrandId, otp => otp.MapFrom(src => src.BrandId))
                .ForPath(dest => dest.BrandName, otp => otp.MapFrom(src => src.BrandName))
                .ForPath(dest => dest.ImageUrl, otp => otp.MapFrom(src => src.ImageUrl))
                 .ForPath(
                    dest => dest.StatusId,
                    otp => otp.MapFrom(src => (int)src.BrandStatus));
        }
    }
}
