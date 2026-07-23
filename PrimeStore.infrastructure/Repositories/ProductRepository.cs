using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.infrastructure.Context;
using PrimeStore.infrastructure.InfrastructureBases;

namespace PrimeStore.infrastructure.Repositories
{
    public class ProductRepository : GenericRepositoryAsync<Product>, IProductRepository
    {
        #region Fields
        private DbSet<Product> _Products;
        #endregion

        #region Constructors
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _Products = dbContext.Set<Product>();
        }
        #endregion

    }
}
