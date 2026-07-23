using PrimeStore.Data.Entities.Identity;
using PrimeStore.infrastructure.InfrastructureBases;

namespace PrimeStore.Infrustructure.Abstracts
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {

    }
}
