namespace PrimeStore.core.Features.Images.Queries.Results
{
    public class GetImagesPaginatedListResponse
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }

        public GetImagesPaginatedListResponse(int imageId, string imageUrl)
        {
            ImageId = imageId;
            ImageUrl = imageUrl;
        }
    }
}
