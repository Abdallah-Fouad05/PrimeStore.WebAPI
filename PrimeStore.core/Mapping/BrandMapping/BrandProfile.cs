using AutoMapper;

namespace PrimeStore.core.Mapping.BrandMapping
{
    public partial class BrandMapping : Profile
    {
        public BrandMapping()
        {
            GetBrandListMapping();
            GetBrandByIdMapping();
            AddBrandMapping();
            EditBrandMapping();
        }
    }
}
