using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeStore.data.Entities.Identity;

namespace PrimeStore.data.Entities.Order
{
    public class Order
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.Order))]
        public virtual User User { get; set; }

        public float TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        [InverseProperty(nameof(OrderStatus.Order))]
        public OrderStatus Status { get; set; }

        [InverseProperty(nameof(OrderItem.Order))]
        public ICollection<OrderItem> OrderItems { get; set; }


        [InverseProperty(nameof(Payment.Order))]
        public Payment.Payment Payment { get; set; }
    }
}
