using AutoMapper;

namespace PrimeStore.core.Mapping.ProductMapping

{
    public partial class ProductProfile : Profile
    {
        public ProductProfile()
        {
            GetProductListMapping();
            GetProductByIdMapping();
            AddProductMapping();
            EditProductMapping();
        }
    }
}
