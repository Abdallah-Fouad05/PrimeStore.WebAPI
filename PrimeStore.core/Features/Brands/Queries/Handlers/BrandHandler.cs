using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Brands.Queries.Models;
using PrimeStore.core.Features.Brands.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Brands.Queries.Handlers
{
    public class BrandHandler : IRequestHandler<GetBrandListQuery, Response<List<GetBrandListResponse>>>,
                                IRequestHandler<GetBrandByIdQuery, Response<GetBrandByIdResponse>>,
                                IRequestHandler<GetActiveBrandListQuery, Response<List<GetBrandListResponse>>>
    {
        #region Feilds
        private readonly IBrandService _BrandService;
        private readonly IMapper _Mapper;
        #endregion

        #region constructor
        public BrandHandler(IBrandService brandService, IMapper mapper)
        {
            _BrandService = brandService;
            _Mapper = mapper;
        }

        #endregion

        #region Handler
        public async Task<Response<List<GetBrandListResponse>>> Handle(GetBrandListQuery request, CancellationToken cancellationToken)
        {
            var brandsList = await _BrandService.GetBrandsListAsync();

            List<GetBrandListResponse> brandMapper = _Mapper.Map<List<GetBrandListResponse>>(brandsList);

            var result = ResponseHandler.Success(brandMapper);

            result.Meta = new { Count = brandMapper.Count() };

            return result;
        }
        public async Task<Response<GetBrandByIdResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var Brand = await _BrandService.GetByIdAsync(request.BrandId);

            if (Brand == null)
                return ResponseHandler.NotFound<GetBrandByIdResponse>("Brand Not Found");

            var BrandMapping = _Mapper.Map<GetBrandByIdResponse>(Brand);

            return ResponseHandler.Success(BrandMapping);
        }

        public async Task<Response<List<GetBrandListResponse>>> Handle(GetActiveBrandListQuery request, CancellationToken cancellationToken)
        {
            var BrandList = await _BrandService.GetActiveBrandsListAsync();

            List<GetBrandListResponse> BrandMapping = _Mapper.Map<List<GetBrandListResponse>>(BrandList);

            var result = ResponseHandler.Success(BrandMapping);

            result.Meta = new { Count = BrandMapping.Count() };

            return result;
        }


        #endregion

    }
}
