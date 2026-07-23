using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.infrastructure.Context;
using PrimeStore.infrastructure.InfrastructureBases;

namespace PrimeStore.infrastructure.Repositories
{
    public class BrandRepository : GenericRepositoryAsync<Brand>, IBrandRepository
    {
        #region Fields
        private DbSet<Brand> _Brands;
        #endregion

        #region Constructors
        public BrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _Brands = dbContext.Set<Brand>();
        }
        #endregion

    }
}
