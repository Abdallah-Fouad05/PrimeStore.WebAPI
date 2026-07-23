using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeStore.data.Entities.Payment
{
    public class PaymentMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MethodId { get; set; }

        [MaxLength(255)]
        public string MethodString { get; set; }

        [InverseProperty(nameof(Payment.Method))]
        public virtual Payment Payment { get; set; }
    }
}
