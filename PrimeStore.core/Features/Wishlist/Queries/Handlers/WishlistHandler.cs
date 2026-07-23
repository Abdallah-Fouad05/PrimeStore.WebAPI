using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Brands.Queries.Results;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.core.Features.Wishlist.Queries.Models;
using PrimeStore.core.Features.Wishlist.Queries.Results;
using PrimeStore.Core.Wrappers;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Wishlist.Queries.Handlers
{
    public class WishlistHandler : IRequestHandler<GetUserWishlistpaginatedListQuery, PaginatedResult<GetUserwishlistPaginatedListResponse>>,
                                   IRequestHandler<IsProductInUserWishlistQuery, bool>
    {
        #region Fields
        private readonly IWishlistService _WishlistService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public WishlistHandler(IWishlistService wishlistService, IMapper mapper)
        {
            _WishlistService = wishlistService;
            _mapper = mapper;
        }
        #endregion

        #region Handler
        public async Task<PaginatedResult<GetUserwishlistPaginatedListResponse>> Handle(GetUserWishlistpaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<data.Entities.Wishlist, GetUserwishlistPaginatedListResponse>> expression = e => new GetUserwishlistPaginatedListResponse(
                    e.WishlistId,
                    e.CreatedAt,
                    new GetProductListResponse(
                        e.Product.ProductId,
                        e.Product.Title,
                        e.Product.Description,
                        new CategoryResponse
                        {
                            CategoryId = e.Product.Category.CategoryId,
                            CategoryName = e.Product.Category.CategoryName,
                            ImageUrl = e.Product.Category.ImageUrl,
                            ParentCategory = e.Product.Category.ParentCategory == null
                                ? null
                                : new CategoryResponse
                                {
                                    CategoryId = e.Product.Category.ParentCategory.CategoryId,
                                    CategoryName = e.Product.Category.ParentCategory.CategoryName,
                                    ImageUrl = e.Product.Category.ParentCategory.ImageUrl
                                }
                        },
                        new BrandResponse
                        {
                            BrandId = e.Product.Brand.BrandId,
                            BrandName = e.Product.Brand.BrandName,
                            ImageUrl = e.Product.Brand.ImageUrl
                        },
                        e.Product.Price,
                        e.Product.Stock,
                        e.Product.CreatedAt,
                        e.Product.UpdatedAt,
                        e.Product.ProductImages.Select(x => new ProductImageResponse
                        {
                            ImageId = x.ProductImageId,
                            ImageUrl = x.ProductImageUrl,
                            Position = x.position,
                            IsCover = x.IsCover
                        }).ToList(),
                        e.Product.ProductAttributes.Select(x => new ProductAttributeResponse
                        {
                            ProductAttributeId = x.ProductAttributeId,
                            Key = x.Key,
                            Value = x.Value
                        }).ToList(),
                        new StatusResponse
                        {
                            StatusId = e.Product.Status.StatusId,
                            StatusName = e.Product.Status.StatusName
                        }
                    )
                );

            var Queryable = _WishlistService.GetUserWishlistQueryable(request.UserId);

            var paginatedList = await Queryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            paginatedList.Meta = new { Count = paginatedList.Data.Count() };

            return paginatedList;
        }

        public async Task<bool> Handle(IsProductInUserWishlistQuery request, CancellationToken cancellationToken)
        {
            return await _WishlistService.HasUserWishlistedProductAsync(request.UserId, request.ProductId);
        }
        #endregion
    }
}
