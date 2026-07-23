using PrimeStore.core.Features.Categories.Commands.Models;

namespace PrimeStore.core.Mapping.CategoryMapping
{
    public partial class CategoryProfile
    {
        public void AddCategoryMapping()
        {

            CreateMap<AddCategoryCommand, Category>()
                 .ForPath(dest => dest.CategoryName, otp => otp.MapFrom(src => src.CategoryName))
                  .ForPath(dest => dest.ImageUrl, otp => otp.MapFrom(src => src.ImageUrl))
                  .ForPath(dest => dest.ParentCategoryID, otp => otp.MapFrom(src => src.ParentCategoryID));
        }
    }
}
