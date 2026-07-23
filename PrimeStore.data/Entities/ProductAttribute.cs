using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStore.data.Entities
{
    public class ProductAttribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductAttributeId { get; set; }

        [StringLength(500)]
        public required string Key { get; set; }

        [StringLength(500)]
        public required string Value { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Product.ProductAttributes))]
        public virtual Product? Product { get; set; }

    }
}