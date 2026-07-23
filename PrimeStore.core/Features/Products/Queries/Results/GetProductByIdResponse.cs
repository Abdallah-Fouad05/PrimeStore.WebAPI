using PrimeStore.core.Features.Brands.Queries.Results;

namespace PrimeStore.core.Features.Products.Queries.Results
{

    public class UserResponse
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string? ImageUrl { get; set; }
    }
    public class ReviewResponse
    {
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public float Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual UserResponse? User { get; set; }

    }

    public class GetProductByIdResponse
    {

        public GetProductByIdResponse(int productId, string title, string description, CategoryResponse categoryResponse,
            BrandResponse brandResponse, float price, int stock, DateTime createdAt, DateTime? updatedAt, List<ProductImageResponse> images, List<ReviewResponse> reviews, List<ProductAttributeResponse> attributes, StatusResponse productStatus)
        {
            ProductId = productId;
            Title = title;
            Description = description;
            Category = categoryResponse;
            Brand = brandResponse;
            Price = price;
            Stock = stock;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Images = images;
            reviews = reviews;
            Attributes = attributes;
            ProductStatus = productStatus;
        }
        public GetProductByIdResponse() { }

        public int ProductId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public virtual CategoryResponse? Category { get; set; }
        public virtual BrandResponse? Brand { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ProductImageResponse> Images { get; set; }
        public List<ReviewResponse> Reviews { get; set; }
        public List<ProductAttributeResponse> Attributes { get; set; }
        public StatusResponse ProductStatus { get; set; }
    }
}
