using PrimeStore.data.Entities;

namespace PrimeStore.service.Abstracts
{
    public interface IBrandService
    {
        Task<ICollection<Brand>> GetBrandsListAsync();
        Task<ICollection<Brand>> GetActiveBrandsListAsync();
        Task<Brand> GetByIdAsync(int Id);
        IQueryable<Brand> GetBrandQueryable();
        Task<bool> IsBrandExist(int Id);
        Task<string> AddAsync(Brand brand);
        Task<string> UpdateAsync(Brand brand);
        Task<string> DeleteAsync(Brand brand);
        Task<bool> IsBrandNameExist(string brandName);
        Task<bool> IsBrandNameExist(string brandName, int brandId);
    }
}
