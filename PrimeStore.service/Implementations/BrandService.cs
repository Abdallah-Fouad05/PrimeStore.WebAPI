using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities;
using PrimeStore.data.Helper;
using PrimeStore.data.Helper.Status;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.service.Abstracts;

namespace PrimeStore.service.Implementations
{
    public class BrandService : IBrandService
    {
        #region Fields
        private readonly IBrandRepository _BrandRepository;
        #endregion

        #region constructor
        public BrandService(IBrandRepository brandRepository)
        {
            _BrandRepository = brandRepository;
        }
        #endregion

        public async Task<ICollection<Brand>> GetBrandsListAsync()
        {
            return await _BrandRepository.GetTableNoTracking().Include(x => x.Status).ToListAsync();
        }

        public async Task<Brand> GetByIdAsync(int Id)
        {
            return await _BrandRepository.GetTableNoTracking().Include(x => x.Status).FirstOrDefaultAsync(x => x.BrandId == Id);
        }

        public async Task<string> AddAsync(Brand brand)
        {
            await _BrandRepository.AddAsync(brand);
            return ResultString.Success;
        }
        public async Task<string> UpdateAsync(Brand brand)
        {
            await _BrandRepository.UpdateAsync(brand);
            return ResultString.Success;
        }

        public async Task<string> DeleteAsync(Brand brand)
        {

            _BrandRepository.BeginTransaction();
            try
            {
                await _BrandRepository.DeleteAsync(brand);
                await _BrandRepository.CommitAsync();
                return ResultString.Success;
            }
            catch
            {
                _BrandRepository.RollBack();
                return ResultString.Failure;
            }
        }


        public async Task<bool> IsBrandExist(int Id)
        {
            var brand = await GetByIdAsync(Id);

            if (brand == null)
                return false;

            return true;
        }

        public async Task<bool> IsBrandNameExist(string brandName)
        {
            //Check if brand Name is Exist or not
            var existingBrand = await _BrandRepository
                .GetTableNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.BrandName == brandName);

            if (existingBrand == null)
                return false;

            return true;
        }

        public async Task<bool> IsBrandNameExist(string brandName, int brandId)
        {
            //Check if brand Name is Exist or not
            var existingBrand = await _BrandRepository
                .GetTableNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.BrandName == brandName &&
                    x.BrandId != brandId);

            if (existingBrand == null)
                return false;

            return true;
        }

        public IQueryable<Brand> GetBrandQueryable()
        {
            return _BrandRepository.GetTableNoTracking();
        }

        public async Task<ICollection<Brand>> GetActiveBrandsListAsync()
        {
            return await _BrandRepository.GetTableNoTracking().Include(x => x.Status).Where(x => x.StatusId == (int)GenericStatusEnum.Active).ToListAsync();
        }
    }
}
