using Microsoft.EntityFrameworkCore;
using PrimeStore.Data.Entities.Identity;
using PrimeStore.infrastructure.Context;
using PrimeStore.infrastructure.InfrastructureBases;
using PrimeStore.Infrustructure.Abstracts;

namespace SchoolProject.Infrustructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fields
        private DbSet<UserRefreshToken> _UserRefreshToken;
        #endregion

        #region Constructors
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _UserRefreshToken = dbContext.Set<UserRefreshToken>();
        }
        #endregion

        #region Handle Functions

        #endregion
    }
}
