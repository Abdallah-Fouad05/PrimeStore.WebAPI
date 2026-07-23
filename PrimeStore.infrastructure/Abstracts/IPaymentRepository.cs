using PrimeStore.data.Entities.Payment;
using PrimeStore.infrastructure.InfrastructureBases;

namespace PrimeStore.infrastructure.Abstracts
{
    public interface IPaymentRepository : IGenericRepositoryAsync<Payment>
    {

    }
}
