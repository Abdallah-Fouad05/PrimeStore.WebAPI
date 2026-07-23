using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Brands.Queries.Results;
using PrimeStore.core.Features.Products.Queries.Results;
using PrimeStore.core.Features.Reviews.Queries.Models;
using PrimeStore.core.Features.Reviews.Queries.Results;
using PrimeStore.Core.Wrappers;
using PrimeStore.data.Entities;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Reviews.Queries.Handlers
{
    public class ReviewHandler : IRequestHandler<GetProductReviewsQuery, PaginatedResult<GetProductReviewsResponse>>,
                                 IRequestHandler<GetUserReviewsQuery, PaginatedResult<GetUserReviewsResponse>>
    {
        #region Fields
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ReviewHandler(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }
        #endregion

        #region Handler
        public async Task<PaginatedResult<GetProductReviewsResponse>> Handle(GetProductReviewsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Review, GetProductReviewsResponse>> expression = e => new GetProductReviewsResponse(e.ReviewId, e.Comment, e.Rating, e.CreatedAt, e.UpdatedAt, new UserReviewResponse { UserId = e.User.Id, UserName = e.User.UserName, ImageUrl = e.User.ImageUrl });

            var Queryable = _reviewService.GetProductReviewsQueryable(request.ProductId);

            var paginatedList = await Queryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            paginatedList.Meta = new { Count = paginatedList.Data.Count() };

            return paginatedList;
        }

        public async Task<PaginatedResult<GetUserReviewsResponse>> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Review, GetUserReviewsResponse>> expression = e =>
                new GetUserReviewsResponse(
                    e.ReviewId,
                    e.Comment,
                    e.Rating,
                    e.CreatedAt,
                    e.UpdatedAt,
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

            var Queryable = _reviewService.GetUserReviewsQueryable(request.UserId);

            var paginatedList = await Queryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            paginatedList.Meta = new { Count = paginatedList.Data.Count() };

            return paginatedList;
        }
        #endregion
    }
}
