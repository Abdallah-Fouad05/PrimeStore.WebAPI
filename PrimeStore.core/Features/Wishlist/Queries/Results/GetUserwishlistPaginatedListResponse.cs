using PrimeStore.core.Features.Products.Queries.Results;

namespace PrimeStore.core.Features.Wishlist.Queries.Results
{
    public class GetUserwishlistPaginatedListResponse
    {
        public int WishlistId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public GetProductListResponse Product { get; set; }

        public GetUserwishlistPaginatedListResponse(int wishlistId, DateTime? createdAt, GetProductListResponse product)
        {
            WishlistId = wishlistId;
            CreatedAt = createdAt;
            Product = product;
        }

    }
}
