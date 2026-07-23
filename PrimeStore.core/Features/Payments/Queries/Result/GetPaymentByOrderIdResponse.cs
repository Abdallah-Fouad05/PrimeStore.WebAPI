namespace PrimeStore.core.Features.Payments.Queries.Result
{
    public class PaymentMethodResponse
    {
        public int MethodId { get; set; }
        public string MethodName { get; set; }
    }

    public class PaymentStatusResponse
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
    public class GetPaymentByOrderIdResponse
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethodResponse Method { get; set; }
        public PaymentStatusResponse Status { get; set; }
        public string? TransactionId { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
