using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Products.Queries.Models;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.Core.Wrappers;
using PrimeStore.data.Entities;
using PrimeStore.data.Helper.Status;
using PrimeStore.service.Abstracts;
using Serilog;

namespace PrimeStore.core.Features.Products.Queries.Handlers
{
    public class ProductHandler : IRequestHandler<GetProductListQuery, Response<List<GetProductListResponse>>>,
                                  IRequestHandler<GetProductByIdQuery, Response<GetProductByIdResponse>>,
                                  IRequestHandler<GetProductPaginatedListQuery, PaginatedResult<GetProductListResponse>>,
                                  IRequestHandler<GetProductPaginatedListByBrandIdQuery, PaginatedResult<GetProductListResponse>>,
                                  IRequestHandler<GetProductPaginatedListByCategoryIdQuery, PaginatedResult<GetProductListResponse>>,
                                  IRequestHandler<GetActiveProductPaginatedListQuery, PaginatedResult<GetProductListResponse>>
    {
        #region Fields
        private readonly IProductService _ProductService;
        private readonly ICategoryService _CategoryService;
        private readonly IMapper _Mapper;
        #endregion

        #region Constructor
        public ProductHandler(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            _ProductService = productService;
            _CategoryService = categoryService;
            _Mapper = mapper;
        }
        #endregion

        #region Handler Function
        public async Task<Response<List<GetProductListResponse>>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var ProductList = await _ProductService.GetProductsListAsync();

            List<GetProductListResponse> ProductListMapper = _Mapper.Map<List<GetProductListResponse>>(ProductList);

            var result = ResponseHandler.Success(ProductListMapper);

            result.Meta = new { Count = ProductListMapper.Count() };

            return result;
        }


        public async Task<Response<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _ProductService.GetProductByProductIdAsync(request.ProductId);

            if (product == null)
            {
                return ResponseHandler.BadRequest<GetProductByIdResponse>("Product Not Found");
            }

            GetProductByIdResponse ProductMapper = _Mapper.Map<GetProductByIdResponse>(product);

            return ResponseHandler.Success(ProductMapper);
        }


        public async Task<PaginatedResult<GetProductListResponse>> Handle(GetProductPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Product, GetProductListResponse>> expression = e => new GetProductListResponse(e.ProductId, e.Title, e.Description, new CategoryResponse
            {
                CategoryId = e.Category.CategoryId,
                CategoryName = e.Category.CategoryName,
                ImageUrl = e.Category.ImageUrl,
                ParentCategory = e.Category.ParentCategory == null ? null : new CategoryResponse
                {
                    CategoryId = e.Category.ParentCategory.CategoryId,
                    CategoryName = e.Category.ParentCategory.CategoryName,
                    ImageUrl = e.Category.ParentCategory.ImageUrl
                }
            }, new BrandResponse
            {
                BrandId = e.Brand.BrandId,
                BrandName = e.Brand.BrandName,
                ImageUrl = e.Brand.ImageUrl,
            }, e.Price, e.Stock, e.CreatedAt, e.UpdatedAt, e.ProductImages.Select(x => new ProductImageResponse
            {
                ImageId = x.ProductImageId,
                ImageUrl = x.ProductImageUrl,
                Position = x.position,
                IsCover = x.IsCover,
            }).ToList(), e.ProductAttributes.Select(x => new Results.ProductAttributeResponse
            {
                ProductAttributeId = x.ProductAttributeId,
                Key = x.Key,
                Value = x.Value,
            }).ToList(),
            new Brands.Queries.Results.StatusResponse { StatusId = e.Status.StatusId, StatusName = e.Status.StatusName }
            );

            var Queryable = _ProductService.GetProductsQueryable();

            var FilterQueryable = _ProductService.FilterProductPaginatedQueryable(request.OrderBy, request.Search);

            var paginatedList = await FilterQueryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            paginatedList.Meta = new { Count = paginatedList.Data.Count() };

            return paginatedList;
        }

        public async Task<PaginatedResult<GetProductListResponse>> Handle(GetProductPaginatedListByBrandIdQuery request, CancellationToken cancellationToken)
        {

            Expression<Func<Product, GetProductListResponse>> expression = e => new GetProductListResponse(e.ProductId, e.Title, e.Description, new CategoryResponse
            {
                CategoryId = e.Category.CategoryId,
                CategoryName = e.Category.CategoryName,
                ImageUrl = e.Category.ImageUrl,
                ParentCategory = e.Category.ParentCategory == null ? null : new CategoryResponse
                {
                    CategoryId = e.Category.ParentCategory.CategoryId,
                    CategoryName = e.Category.ParentCategory.CategoryName,
                    ImageUrl = e.Category.ParentCategory.ImageUrl
                }
            }, new BrandResponse
            {
                BrandId = e.Brand.BrandId,
                BrandName = e.Brand.BrandName,
                ImageUrl = e.Brand.ImageUrl,
            }, e.Price, e.Stock, e.CreatedAt, e.UpdatedAt, e.ProductImages.Select(x => new ProductImageResponse
            {
                ImageId = x.ProductImageId,
                ImageUrl = x.ProductImageUrl,
                Position = x.position,
                IsCover = x.IsCover,
            }).ToList(), e.ProductAttributes.Select(x => new Results.ProductAttributeResponse
            {
                ProductAttributeId = x.ProductAttributeId,
                Key = x.Key,
                Value = x.Value,
            }).ToList(),
            new Brands.Queries.Results.StatusResponse { StatusId = e.Status.StatusId, StatusName = e.Status.StatusName }
            );

            var Queryable = _ProductService.GetProductsByBrandIdQueryable(request.BrandId);

            var paginatedList = await Queryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            paginatedList.Meta = new { Count = paginatedList.Data.Count() };

            return paginatedList;
        }

        public async Task<PaginatedResult<GetProductListResponse>?> Handle(GetProductPaginatedListByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            Category category = await _CategoryService.GetByIDAsync(request.CategoryId);

            if (category == null)
                return null;

            Expression<Func<Product, GetProductListResponse>> expression = e => new GetProductListResponse(e.ProductId, e.Title, e.Description, new CategoryResponse
            {
                CategoryId = e.Category.CategoryId,
                CategoryName = e.Category.CategoryName,
                ImageUrl = e.Category.ImageUrl,
                ParentCategory = e.Category.ParentCategory == null ? null : new CategoryResponse
                {
                    CategoryId = e.Category.ParentCategory.CategoryId,
                    CategoryName = e.Category.ParentCategory.CategoryName,
                    ImageUrl = e.Category.ParentCategory.ImageUrl
                }
            }, new BrandResponse
            {
                BrandId = e.Brand.BrandId,
                BrandName = e.Brand.BrandName,
                ImageUrl = e.Brand.ImageUrl,
            }, e.Price, e.Stock, e.CreatedAt, e.UpdatedAt, e.ProductImages.Select(x => new ProductImageResponse
            {
                ImageId = x.ProductImageId,
                ImageUrl = x.ProductImageUrl,
                Position = x.position,
                IsCover = x.IsCover,
            }).ToList(), e.ProductAttributes.Select(x => new Results.ProductAttributeResponse
            {
                ProductAttributeId = x.ProductAttributeId,
                Key = x.Key,
                Value = x.Value,
            }).ToList(),
            new Brands.Queries.Results.StatusResponse { StatusId = e.Status.StatusId, StatusName = e.Status.StatusName }
            );

            var Queryable = _ProductService.GetProductsByCategoryIdQueryable(category);

            var paginatedList = await Queryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            paginatedList.Meta = new { Count = paginatedList.Data.Count() };

            return paginatedList;
        }

        public async Task<PaginatedResult<GetProductListResponse>> Handle(GetActiveProductPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Product, GetProductListResponse>> expression = e => new GetProductListResponse(e.ProductId, e.Title, e.Description, new CategoryResponse
            {
                CategoryId = e.Category.CategoryId,
                CategoryName = e.Category.CategoryName,
                ImageUrl = e.Category.ImageUrl,
                ParentCategory = e.Category.ParentCategory == null ? null : new CategoryResponse
                {
                    CategoryId = e.Category.ParentCategory.CategoryId,
                    CategoryName = e.Category.ParentCategory.CategoryName,
                    ImageUrl = e.Category.ParentCategory.ImageUrl
                }
            }, new BrandResponse
            {
                BrandId = e.Brand.BrandId,
                BrandName = e.Brand.BrandName,
                ImageUrl = e.Brand.ImageUrl,
            }, e.Price, e.Stock, e.CreatedAt, e.UpdatedAt, e.ProductImages.Select(x => new ProductImageResponse
            {
                ImageId = x.ProductImageId,
                ImageUrl = x.ProductImageUrl,
                Position = x.position,
                IsCover = x.IsCover,
            }).ToList(), e.ProductAttributes.Select(x => new Results.ProductAttributeResponse
            {
                ProductAttributeId = x.ProductAttributeId,
                Key = x.Key,
                Value = x.Value,
            }).ToList(),
 new Brands.Queries.Results.StatusResponse { StatusId = e.Status.StatusId, StatusName = e.Status.StatusName }
 );

            var Queryable = _ProductService.GetProductsQueryable().Where(x => x.StatusId == (int)GenericStatusEnum.Active);

            var FilterQueryable = _ProductService.FilterProductPaginatedQueryable(request.OrderBy, request.Search);

            var paginatedList = await FilterQueryable.Where(x => x.StatusId == (int)GenericStatusEnum.Active).Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            paginatedList.Meta = new { Count = paginatedList.Data.Count() };

            Log.Information("Get Active Products");

            return paginatedList;
        }

        #endregion
    }
}
