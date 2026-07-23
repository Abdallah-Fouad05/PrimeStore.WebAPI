using AutoMapper;

namespace PrimeStore.core.Mapping.CategoryMapping
{
    public partial class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            GetCategoriesListMapping();
            AddCategoryMapping();
            EditCategoryMapping();
        }
    }
}
