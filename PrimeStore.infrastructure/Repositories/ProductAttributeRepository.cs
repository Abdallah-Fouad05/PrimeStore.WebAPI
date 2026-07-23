using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.infrastructure.Context;
using PrimeStore.infrastructure.InfrastructureBases;

namespace PrimeStore.infrastructure.Repositories
{
    public class ProductAttributeRepository : GenericRepositoryAsync<ProductAttribute>, IProductAttributeRepository
    {
        #region Fields
        private DbSet<ProductAttribute> _ProductAttributes;
        #endregion

        #region Constructors
        public ProductAttributeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _ProductAttributes = dbContext.Set<ProductAttribute>();
        }
        #endregion

    }
}
