using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Images.Queries.Models;
using PrimeStore.core.Features.Images.Queries.Results;
using PrimeStore.Core.Wrappers;
using PrimeStore.data.Entities;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Images.Queries.Handlers
{
    public class ImageHandler : IRequestHandler<GetImagesPaginatedListQuery, PaginatedResult<GetImagesPaginatedListResponse>>
    {
        #region Feilds
        private readonly IImageService _ImageService;
        private readonly IMapper _Mapper;
        #endregion

        #region constructor
        public ImageHandler(IImageService imageService, IMapper mapper)
        {
            _ImageService = imageService;
            _Mapper = mapper;
        }
        #endregion
        public async Task<PaginatedResult<GetImagesPaginatedListResponse>> Handle(GetImagesPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Image, GetImagesPaginatedListResponse>> expression = e => new GetImagesPaginatedListResponse(e.ImageId, e.ImageUrl);

            var Queryable = _ImageService.GetImagesQueryable();

            var paginatedList = await Queryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            paginatedList.Meta = new { Count = paginatedList.Data.Count() };

            return paginatedList;
        }
    }
}
