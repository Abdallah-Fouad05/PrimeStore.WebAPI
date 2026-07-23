using Microsoft.EntityFrameworkCore;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.infrastructure.Context;
using PrimeStore.infrastructure.InfrastructureBases;

namespace PrimeStore.infrastructure.Repositories
{
    public class CategoryRepository : GenericRepositoryAsync<Category>, ICategoryRepository
    {
        #region Fields
        private DbSet<Category> _categories;
        #endregion
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _categories = _dbContext.Set<Category>();
        }
    }
}
