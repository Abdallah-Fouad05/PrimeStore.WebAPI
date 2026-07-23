using PrimeStore.core.Features.Brands.Queries.Results;

namespace PrimeStore.core.Features.Products.Queries.Results
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public CategoryResponse? ParentCategory { get; set; }
    }
    public class BrandResponse
    {
        public int BrandId { get; set; }
        public required string BrandName { get; set; }
        public string? ImageUrl { get; set; }
    }
    public class ProductImageResponse
    {
        public int ImageId { get; set; }
        public string ImageUrl
        {
            get; set;
        }

        public int Position { get; set; }
        public bool IsCover { get; set; }
    }
    public class ProductAttributeResponse
    {

        public int ProductAttributeId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class GetProductListResponse
    {
        public GetProductListResponse(int productId, string title, string description, CategoryResponse categoryResponse,
            BrandResponse brandResponse, float price, int stock, DateTime createdAt,
            DateTime? updatedAt, List<ProductImageResponse> images, List<ProductAttributeResponse> attributes, StatusResponse? productstatus)
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
            Attributes = attributes;
            ProductStatus = productstatus;
        }
        public GetProductListResponse() { }

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
        public List<ProductAttributeResponse> Attributes { get; set; }

        public StatusResponse ProductStatus { get; set; }
    }
}
