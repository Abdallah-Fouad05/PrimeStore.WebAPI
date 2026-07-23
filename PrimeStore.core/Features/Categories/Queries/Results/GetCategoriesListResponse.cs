namespace PrimeStore.core.Features.Categories.Queries.Results
{
    public class GetCategoriesListResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public List<GetCategoriesListResponse>? ChildCategories { get; set; }

    }
}
