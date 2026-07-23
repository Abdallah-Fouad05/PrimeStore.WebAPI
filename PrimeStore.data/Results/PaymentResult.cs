using PrimeStore.data.Helper;
using PrimeStore.data.Helper.Status;

namespace PrimeStore.data.Results
{
    public class PaymentResult
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethodEnum MethodtId { get; set; }
        public int StatusId { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public string? TransactionId { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
