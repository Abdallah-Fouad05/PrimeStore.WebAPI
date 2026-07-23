using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeStore.data.Entities.Status
{
    public class GenericStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }

        [StringLength(50)]
        public required string StatusName { get; set; }

        [InverseProperty(nameof(Brand.Status))]
        public List<Brand> Brands { get; set; } = new List<Brand>();

        [InverseProperty(nameof(Product.Status))]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
