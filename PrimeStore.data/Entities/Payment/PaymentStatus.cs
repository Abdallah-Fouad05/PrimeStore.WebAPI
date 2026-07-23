using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeStore.data.Entities.Payment
{
    public class PaymentStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }

        [MaxLength(255)]
        public string StatusName { get; set; }

        [InverseProperty(nameof(Payment.Status))]
        public virtual Payment Payment { get; set; }
    }
}
