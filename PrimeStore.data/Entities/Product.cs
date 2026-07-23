using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeStore.data.Entities.Order;
using PrimeStore.data.Entities.Status;

namespace PrimeStore.data.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [StringLength(500)]
        public required string Title { get; set; }

        public string? Description { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(Category.Products))]
        public virtual Category? Category { get; set; }

        public int BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        [InverseProperty(nameof(Brand.Products))]
        public virtual Brand? Brand { get; set; }

        public float Price { get; set; }

        public int Stock { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [InverseProperty(nameof(ProductImage.Product))]
        public virtual ICollection<ProductImage>? ProductImages { get; set; } = new List<ProductImage>();

        [InverseProperty(nameof(Review.Product))]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [InverseProperty(nameof(ProductAttribute.Product))]
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new HashSet<ProductAttribute>();

        [InverseProperty(nameof(Wishlist.Product))]
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

        [InverseProperty(nameof(CartItem.Product))]
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        [InverseProperty(nameof(GenericStatus.Products))]
        public virtual GenericStatus? Status { get; set; }

        [InverseProperty(nameof(OrderItem.Product))]
        public virtual OrderItem OrderItem { get; set; }


    }
}
