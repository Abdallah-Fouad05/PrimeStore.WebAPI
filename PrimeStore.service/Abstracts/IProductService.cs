using PrimeStore.data.Entities;
using PrimeStore.data.Helper;

namespace PrimeStore.service.Abstracts
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsListAsync();
        Task<List<Product>> GetActiveProductsListAsync();
        Task<Product?> GetProductByProductIdAsync(int Id);
        IQueryable<Product> GetProductsQueryable();
        Task<Product> GetByIDAsync(int id);
        Task<string> AddAsync(Product product);
        Task<string> UpdateAsync(Product product);
        Task<string> DeleteAsync(Product product);
        IQueryable<Product> FilterProductPaginatedQueryable(ProductOrderingEnum orderyby, string search);
        Task<bool> IsTitleExist(string Title);
        Task<bool> IsTitleExist(string Title, int ProductId);
        IQueryable<Product> GetProductsByBrandIdQueryable(int BrandId);
        IQueryable<Product> GetProductsByCategoryIdQueryable(Category Category);
    }
}
