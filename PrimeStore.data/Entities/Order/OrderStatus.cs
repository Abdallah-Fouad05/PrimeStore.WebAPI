using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeStore.data.Entities.Order
{
    public class OrderStatus
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }

        public string StatusName { get; set; }

        [InverseProperty(nameof(Order.Status))]
        public virtual Order Order { get; set; }
    }
}
