using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeStore.data.Entities.Payment
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(Order.Payment))]
        public Order.Order Order { get; set; }
        public decimal Amount { get; set; }

        public int MethodtId { get; set; }

        [ForeignKey(nameof(MethodtId))]
        [InverseProperty(nameof(PaymentMethod.Payment))]

        public PaymentMethod Method { get; set; }

        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        [InverseProperty(nameof(PaymentStatus.Payment))]
        public PaymentStatus Status { get; set; }
        public string? TransactionId { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
