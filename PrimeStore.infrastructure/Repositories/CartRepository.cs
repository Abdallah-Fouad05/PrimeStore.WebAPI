using PrimeStore.data.Entities;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.infrastructure.Context;
using PrimeStore.infrastructure.InfrastructureBases;

namespace PrimeStore.infrastructure.Repositories
{
    public class CartRepository : GenericRepositoryAsync<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
