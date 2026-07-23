namespace PrimeStore.core.Features.Brands.Queries.Results
{

    public class GetBrandByIdResponse
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string? ImageUrl { get; set; }
        public StatusResponse BrandStatus { get; set; }
    }
}
