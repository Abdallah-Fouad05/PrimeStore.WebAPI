using PrimeStore.core.Features.Brands.Commands.Models;
using PrimeStore.data.Entities;

namespace PrimeStore.core.Mapping.BrandMapping
{
    public partial class BrandMapping
    {
        public void AddBrandMapping()
        {
            CreateMap<AddBrandCommand, Brand>()
                .ForPath(
                    dest => dest.BrandName,
                    opt => opt.MapFrom(src => src.BrandName))
                .ForPath(
                    dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.ImageUrl))
                .ForPath(
                    dest => dest.StatusId,
                    otp => otp.MapFrom(src => (int)src.BrandStatus));
        }
    }
}
