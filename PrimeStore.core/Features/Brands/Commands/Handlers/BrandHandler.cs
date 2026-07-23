using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Brands.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Entities;
using PrimeStore.data.Helper;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Brands.Commands.Handlers
{
    public class BrandHandler : IRequestHandler<AddBrandCommand, Response<string>>,
                                IRequestHandler<EditBrandCommand, Response<string>>,
                                IRequestHandler<DeleteBrandCommand, Response<string>>
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
        public async Task<Response<string>> Handle(AddBrandCommand request, CancellationToken cancellationToken)
        {
            var NewBrand = _Mapper.Map<Brand>(request);

            var result = await _BrandService.AddAsync(NewBrand);
            if (result == ResultString.Success)
            {
                return ResponseHandler.Created("Added Successfully");
            }
            return ResponseHandler.BadRequest<string>();

        }

        public async Task<Response<string>> Handle(EditBrandCommand request, CancellationToken cancellationToken)
        {

            Brand? BrandResult = await _BrandService.GetByIdAsync(request.BrandId);

            if (BrandResult == null)
            {
                return ResponseHandler.NotFound<string>("Brand Not Found");
            }

            var BrandMapper = _Mapper.Map(request, BrandResult);

            string result = await _BrandService.UpdateAsync(BrandMapper);

            if (result == ResultString.Success)
            {
                return ResponseHandler.Success("Edited Successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>();
            }
        }

        public async Task<Response<string>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {

            Brand? BrandResult = await _BrandService.GetByIdAsync(request.BrandId);

            if (BrandResult == null)
            {
                return ResponseHandler.NotFound<string>("Brand Not Found");
            }

            string result = await _BrandService.DeleteAsync(BrandResult);

            if (result == ResultString.Success)
            {
                return ResponseHandler.Success("Deleted Successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>();
            }
        }
        #endregion
    }
}
