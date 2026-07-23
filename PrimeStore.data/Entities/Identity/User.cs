using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using PrimeStore.data.Entities.Status;
using PrimeStore.Data.Entities.Identity;

namespace PrimeStore.data.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        [StringLength(500)]
        public required string FullName { get; set; }
        public required string Address { get; set; }

        [StringLength(500)]
        public required string Country { get; set; }

        public string? ImageUrl { get; set; }

        [EncryptColumn]
        public string? Code { get; set; }


        [InverseProperty(nameof(Review.User))]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [InverseProperty(nameof(Wishlist.User))]
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

        [InverseProperty(nameof(Cart.User))]
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = new List<UserRefreshToken>();

        [InverseProperty(nameof(Order.User))]
        public virtual Order.Order Order { get; set; }

        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        [InverseProperty(nameof(UserStatus.users))]
        public virtual UserStatus? UserStatus { get; set; }




    }
}
