using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Helper;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.service.Abstracts;

namespace PrimeStore.service.Implementations
{
    public class CategoryService : ICategoryService
    {
        #region Fields
        private readonly ICategoryRepository _CategoryRepository;
        #endregion

        #region Constructor
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _CategoryRepository = categoryRepository;
        }
        #endregion

        #region Services

        public async Task<string> AddAsync(Category category)
        {
            await _CategoryRepository.AddAsync(category);
            return ResultString.Success;
        }

        public async Task<string> DeleteAsync(Category category)
        {
            var trans = _CategoryRepository.BeginTransaction();
            try
            {
                await _CategoryRepository.DeleteAsync(category);
                trans.Commit();
                return ResultString.Success;
            }
            catch
            {
                trans.Rollback();
                return ResultString.Failure;
            }
        }

        public async Task<Category> GetByIDAsync(int id)
        {
            return await _CategoryRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public async Task<List<Category>> GetCategoriesListAsync()
        {
            return await _CategoryRepository.GetTableNoTracking().ToListAsync();
        }
        public IQueryable<Category> GetCategoryQueryable()
        {
            return _CategoryRepository.GetTableNoTracking();
        }

        public async Task<bool> IsCategoryNameExist(string CategoryName)
        {
            var result = await _CategoryRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.CategoryName == CategoryName);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsCategoryNameExist(string CategoryName, int CategoryId)
        {
            var result = await _CategoryRepository.GetTableNoTracking()
                                    .FirstOrDefaultAsync(x => x.CategoryName == CategoryName && x.CategoryId != CategoryId);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> UpdateAsync(Category category)
        {
            await _CategoryRepository.UpdateAsync(category);
            return ResultString.Success;
        }
        #endregion
    }
}
