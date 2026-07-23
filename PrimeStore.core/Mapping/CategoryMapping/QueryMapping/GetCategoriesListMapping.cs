using PrimeStore.core.Features.Categories.Queries.Results;

namespace PrimeStore.core.Mapping.CategoryMapping
{
    public partial class CategoryProfile
    {
        public void GetCategoriesListMapping()
        {
            CreateMap<Category, GetCategoriesListResponse>()
                .ForPath(dest => dest.CategoryId, otp => otp.MapFrom(src => src.CategoryId))
                 .ForPath(dest => dest.CategoryName, otp => otp.MapFrom(src => src.CategoryName))
                  .ForPath(dest => dest.ImageUrl, otp => otp.MapFrom(src => src.ImageUrl))
                  .ForPath(dest => dest.ChildCategories, otp => otp.MapFrom(src => src.ChildCategories));

        }
    }

}
