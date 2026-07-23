using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Images.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Helper;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Images.Commands.Handlers
{
    public class ImageHandler : IRequestHandler<AddImageCommand, Response<string>>,
                                IRequestHandler<DeleteImageCommand, Response<string>>
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

        public async Task<Response<string>> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            var response = await _ImageService.AddImageAsync(request.Image);
            switch (response)
            {
                case ResultString.Success: return ResponseHandler.Success("Added Image Successfully");
                case "NoImage": return ResponseHandler.BadRequest<string>("NoImage");
                case "FailedToUploadImage": return ResponseHandler.BadRequest<string>("FailedToUploadImage");
                case "FailedInAdd": return ResponseHandler.BadRequest<string>("AddedFaild");
                default:
                    return ResponseHandler.BadRequest<string>(ResultString.Failure);
            }
        }

        public async Task<Response<string>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var response = await _ImageService.DeleteImageAsync(request.ImageId);
            switch (response)
            {
                case ResultString.Success:
                    return ResponseHandler.Success("Delete Successfully");
                case ResultString.NotFound:
                    return ResponseHandler.NotFound<string>("Image Not Found");
                default:
                    return ResponseHandler.BadRequest<string>(ResultString.Failure);
            }
        }
        #endregion


    }
}
