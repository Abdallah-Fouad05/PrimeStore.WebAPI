using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.infrastructure.Context;
using PrimeStore.infrastructure.InfrastructureBases;

namespace PrimeStore.infrastructure.Repositories
{
    public class ProductImageRepository : GenericRepositoryAsync<ProductImage>, IProductImageRepository
    {
        #region Fields
        private DbSet<ProductImage> _ProductImages;
        #endregion

        #region Constructors
        public ProductImageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _ProductImages = dbContext.Set<ProductImage>();
        }

        #endregion

    }
}
