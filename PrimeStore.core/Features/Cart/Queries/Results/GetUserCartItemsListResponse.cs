namespace PrimeStore.core.Features.Cart.Queries.Results
{
    public class ProductBrand
    {
        public int BrandId { get; set; }

        public string Name { get; set; }
    }

    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
    public class CartItemProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float Price { get; set; }
        public ProductBrand Brand { get; set; }
        public ProductCategory Category { get; set; }
        public string ProductImage { get; set; }

    }
    public class GetUserCartItemsListResponse
    {
        public int CartItemId { get; set; }

        public CartItemProduct Product { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }

    }
}
