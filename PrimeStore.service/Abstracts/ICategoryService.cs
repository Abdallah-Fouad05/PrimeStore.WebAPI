namespace PrimeStore.service.Abstracts
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesListAsync();
        IQueryable<Category> GetCategoryQueryable();
        Task<Category> GetByIDAsync(int id);
        Task<string> AddAsync(Category category);
        Task<string> UpdateAsync(Category category);
        Task<string> DeleteAsync(Category category);
        Task<bool> IsCategoryNameExist(string CategoryName);
        Task<bool> IsCategoryNameExist(string CategoryName, int CategoryId);
    }
}
