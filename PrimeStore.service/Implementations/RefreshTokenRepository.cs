using Microsoft.EntityFrameworkCore;
using PrimeStore.Data.Entities.Identity;
using PrimeStore.infrastructure.Context;
using PrimeStore.infrastructure.InfrastructureBases;
using PrimeStore.Infrustructure.Abstracts;

namespace PrimeStore.Infrustructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fields
        private DbSet<UserRefreshToken> userRefreshToken;
        #endregion

        #region Constructors
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            userRefreshToken = dbContext.Set<UserRefreshToken>();
        }
        #endregion

        #region Handle Functions

        #endregion
    }
}
