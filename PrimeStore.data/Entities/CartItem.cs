using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeStore.data.Entities
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int CartId { get; set; }

        [ForeignKey(nameof(CartId))]
        [InverseProperty(nameof(Cart.CartItems))]
        public virtual Cart? cart { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Product.CartItems))]
        public virtual Product? Product { get; set; }
        public float Total { get; set; }

    }
}
