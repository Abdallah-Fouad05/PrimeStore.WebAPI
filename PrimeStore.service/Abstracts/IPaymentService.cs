using PrimeStore.data.Entities.Payment;
using PrimeStore.data.Results;

namespace PrimeStore.service.Abstracts
{
    public interface IPaymentService
    {
        Task<ICollection<Payment>> GetPaymentsByPaymentIdAsync(int OrderId);
        Task<string> AddUserPaymentAsync(PaymentResult payment);
        Task<string> UpdatePaymentStatusAsync(int payment, int StatusId);
    }
}
