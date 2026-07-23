using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Products.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Entities;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Products.Commands.Handlers
{
    public class ProductHandler : IRequestHandler<AddProductCommand, Response<string>>,
                                  IRequestHandler<EditProductCommand, Response<string>>
    {
        #region Fields
        private readonly IProductService _ProductService;
        private readonly IMapper _Mapper;
        #endregion

        #region Constructor
        public ProductHandler(IProductService productService, IMapper mapper)
        {
            _ProductService = productService;
            _Mapper = mapper;
        }
        #endregion

        #region Handle Function
        public async Task<Response<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var ProductMapper = _Mapper.Map<Product>(request);

            string result = await _ProductService.AddAsync(ProductMapper);
            if (result == "Success")
            {
                return ResponseHandler.Created("Added Successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>();
            }
        }

        public async Task<Response<string>> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {

            Product? productResult = await _ProductService.GetByIDAsync(request.ProductId);

            if (productResult == null)
            {
                return ResponseHandler.NotFound<string>("Product Not Found");

            }

            var ProductMapper = _Mapper.Map(request, productResult);

            string result = await _ProductService.UpdateAsync(ProductMapper);

            if (result == "Success")
            {
                return ResponseHandler.Success("Edited Successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>();
            }
        }

        public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? productResult = await _ProductService.GetByIDAsync(request.ProductId);

            if (productResult == null)
            {
                return ResponseHandler.NotFound<string>("Product Not Found");
            }

            string result = await _ProductService.DeleteAsync(productResult);

            if (result == "Success")
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
