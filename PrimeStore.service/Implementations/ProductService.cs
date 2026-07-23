using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities;
using PrimeStore.data.Helper;
using PrimeStore.data.Helper.Status;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.service.Abstracts;

namespace PrimeStore.service.Implementations
{
    public class ProductService : IProductService
    {

        #region Fields
        private readonly IProductRepository _ProductRepository;
        private readonly IProductImageRepository _ProductImageRepository;
        private readonly IProductAttributeRepository _ProductAttributeRepository;
        #endregion

        #region constructors
        public ProductService(IProductRepository productRepository, IProductImageRepository productImageRepository, IProductAttributeRepository productAttributeRepository)
        {
            _ProductRepository = productRepository;
            _ProductImageRepository = productImageRepository;
            _ProductAttributeRepository = productAttributeRepository;
        }
        #endregion

        #region function
        private List<int> GetCategoryIds(Category category)
        {
            var ids = new List<int> { category.CategoryId };

            if (category.ChildCategories != null)
            {
                foreach (var child in category.ChildCategories)
                {
                    ids.AddRange(GetCategoryIds(child));
                }
            }

            return ids;
        }
        #endregion

        #region Handler Function
        public async Task<List<Product>> GetProductsListAsync()
        {
            return await _ProductRepository.GetTableNoTracking()
                            .Include(x => x.Category).ThenInclude(x => x.ParentCategory)
                            .Include(x => x.Brand).Include(x => x.ProductImages)
                            .Include(x => x.ProductAttributes)
                            .Include(x => x.Status)
                            .ToListAsync();
        }

        public async Task<List<Product>> GetActiveProductsListAsync()
        {
            return await _ProductRepository.GetTableNoTracking()
                            .Include(x => x.Category).ThenInclude(x => x.ParentCategory)
                            .Include(x => x.Brand).Include(x => x.ProductImages)
                            .Include(x => x.ProductAttributes)
                            .Include(x => x.Status).Where(x => x.StatusId == (int)GenericStatusEnum.Active)
                            .ToListAsync();
        }
        public async Task<Product> GetByIDAsync(int id)
        {
            var product = await _ProductRepository.GetByIdAsync(id);
            return product;
        }

        public IQueryable<Product> GetProductsQueryable()
        {
            return _ProductRepository.GetTableNoTracking().Include(x => x.Category).ThenInclude(x => x.ParentCategory).
                Include(x => x.Brand).Include(x => x.ProductImages).Include(x => x.ProductAttributes)
                .Include(x => x.Status).AsQueryable();
        }

        public async Task<Product?> GetProductByProductIdAsync(int Id)
        {
            var product = await _ProductRepository.GetTableNoTracking()
                            .Include(x => x.Category).ThenInclude(x => x.ParentCategory)
                            .Include(x => x.Brand).Include(x => x.ProductImages)
                            .Include(x => x.Reviews).ThenInclude(x => x.User)
                            .Include(x => x.ProductAttributes)
                            .Include(x => x.Status)
                            .FirstOrDefaultAsync(x => x.ProductId == Id);

            return product;
        }
        public async Task<string> AddAsync(Product product)
        {
            var trans = await _ProductRepository.BeginTransactionAsync();
            try
            {
                product.CreatedAt = DateTime.UtcNow;

                //add product
                Product AddedProduct = await _ProductRepository.AddAsync(product);

                //add product image

                List<ProductImage> productImages = product.ProductImages
                    .Select(x => new ProductImage { ProductId = AddedProduct.ProductId, ProductImageUrl = x.ProductImageUrl, position = x.position, IsCover = x.IsCover })
                    .ToList();

                await _ProductImageRepository.AddRangeAsync(productImages);


                //add product Attribute

                List<ProductAttribute> productAttributes = product.ProductAttributes.
                    Select(x => new ProductAttribute { ProductId = AddedProduct.ProductId, Key = x.Key, Value = x.Value }).ToList();

                await _ProductAttributeRepository.AddRangeAsync(productAttributes);

                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Failure";
            }
        }
        public async Task<string> UpdateAsync(Product product)
        {
            var trans = await _ProductRepository.BeginTransactionAsync();
            try
            {
                product.UpdatedAt = DateTime.UtcNow;

                //update product
                await _ProductRepository.UpdateAsync(product);

                //delete old product images
                List<ProductImage> OldproductImages = await _ProductImageRepository.GetTableAsTracking().Where(x => x.ProductId == product.ProductId).ToListAsync();
                await _ProductImageRepository.DeleteRangeAsync(OldproductImages);

                //add Product images
                List<ProductImage> productImages = product.ProductImages
                    .Select(x => new ProductImage { ProductId = product.ProductId, ProductImageUrl = x.ProductImageUrl, position = x.position, IsCover = x.IsCover })
                    .ToList();

                await _ProductImageRepository.AddRangeAsync(productImages);

                //delete old product images
                List<ProductAttribute> OldproductAttributes = await _ProductAttributeRepository.GetTableAsTracking().Where(x => x.ProductId == product.ProductId).ToListAsync();
                await _ProductAttributeRepository.DeleteRangeAsync(OldproductAttributes);

                //add product Attribute
                List<ProductAttribute> productAttributes = product.ProductAttributes.
                    Select(x => new ProductAttribute { ProductId = product.ProductId, Key = x.Key, Value = x.Value }).ToList();

                await _ProductAttributeRepository.AddRangeAsync(productAttributes);

                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Failure";
            }
        }
        public async Task<string> DeleteAsync(Product product)
        {
            var trans = await _ProductRepository.BeginTransactionAsync();
            try
            {
                await _ProductRepository.DeleteAsync(product);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Failure";
            }
        }

        public async Task<bool> IsTitleExist(string Title)
        {
            //Check if title is Exist or not
            var ProductResult = await _ProductRepository.GetTableNoTracking().Where(x => x.Title == Title).FirstOrDefaultAsync();

            if (ProductResult == null)
                return false;

            return true;
        }
        public async Task<bool> IsTitleExist(string Title, int ProductId)
        {
            //Check if title is Exist or not
            var existingProduct = await _ProductRepository
                .GetTableNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.Title == Title &&
                    x.ProductId != ProductId);

            if (existingProduct == null)
                return false;

            return true;
        }

        public IQueryable<Product> FilterProductPaginatedQueryable(ProductOrderingEnum orderby, string search)
        {
            var querable = _ProductRepository.GetTableNoTracking().Include(x => x.Category).ThenInclude(x => x.ParentCategory)
                .Include(x => x.Brand).Include(x => x.ProductImages)
                .Include(x => x.Status)
                .AsQueryable();

            if (search != null)
            {
                querable = querable.Where(x => x.Title.Contains(search));
            }


            switch (orderby)
            {
                case ProductOrderingEnum.CreatedDate:
                    querable = querable.OrderBy(x => x.CreatedAt);
                    break;
                case ProductOrderingEnum.Title:
                    querable = querable.OrderBy(x => x.Title);
                    break;
                case ProductOrderingEnum.Price:
                    querable = querable.OrderBy(x => x.Price);
                    break;
                default:
                    querable = querable.OrderBy(x => x.CreatedAt);
                    break;
            }

            return querable;

        }

        public IQueryable<Product> GetProductsByBrandIdQueryable(int BrandId)
        {
            return _ProductRepository.GetTableNoTracking().Include(x => x.Brand)
              .Include(x => x.Category).ThenInclude(x => x.ParentCategory)
              .Where(x => x.BrandId == BrandId).Include(x => x.ProductImages)
              .Include(x => x.Status).Where(x => x.StatusId == (int)GenericStatusEnum.Active)
              .AsQueryable();
        }

        public IQueryable<Product> GetProductsByCategoryIdQueryable(Category category)
        {
            var querable = _ProductRepository.GetTableNoTracking()
                .Include(x => x.Brand).Include(x => x.ProductImages)
                .Include(x => x.Category).ThenInclude(x => x.ParentCategory)
                .Include(x => x.Status).Where(x => x.StatusId == (int)GenericStatusEnum.Active)
                .AsQueryable();

            var categoryIds = GetCategoryIds(category);

            querable = querable.Where(x => categoryIds.Contains(x.CategoryId));

            return querable;
        }

        #endregion

    }
}
