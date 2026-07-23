using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeStore.data.Entities.Status;

namespace PrimeStore.data.Entities
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }

        [StringLength(500)]
        public required string BrandName { get; set; }
        public string? ImageUrl { get; set; }

        [InverseProperty(nameof(Product.Brand))]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        [InverseProperty(nameof(GenericStatus.Brands))]
        public virtual GenericStatus? Status { get; set; }
    }
}
