namespace PrimeStore.core.Features.Brands.Queries.Results
{
    public class StatusResponse
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
    public class GetBrandListResponse
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string? ImageUrl { get; set; }
        public StatusResponse BrandStatus { get; set; }
    }
}
