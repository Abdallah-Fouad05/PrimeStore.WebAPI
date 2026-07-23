using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Reviews.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.data.Entities;
using PrimeStore.data.Helper;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Reviews.Commands.Handlers
{
    public class ReviewHandler : IRequestHandler<AddReviewCommand, Response<string>>,
                                 IRequestHandler<EditReviewCommand, Response<string>>,
                                 IRequestHandler<DeleteReviewCommand, Response<string>>

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
        public async Task<Response<string>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Review>(request);

            var result = await _reviewService.AddProductReview(review);

            if (result == ResultString.Success)
            {
                return ResponseHandler.Success("Review Added Successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>("Failed to Add Review");
            }
        }

        public async Task<Response<string>> Handle(EditReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewResult = await _reviewService.GetReviewByIdAsync(request.ReviewId);

            if (reviewResult == null)
                return ResponseHandler.NotFound<string>("Review not found");

            var ReviewMapping = _mapper.Map(request, reviewResult);

            var result = await _reviewService.UpdateProductReview(ReviewMapping);

            if (result == ResultString.Success)
            {
                return ResponseHandler.Success("Review updated successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>("Failed to update review");
            }
        }

        public async Task<Response<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetReviewByIdAsync(request.ReviewId);

            if (review == null)
                return ResponseHandler.NotFound<string>("Review not found");

            var result = await _reviewService.DeleteProductReview(review);

            if (result == ResultString.Success)
            {
                return ResponseHandler.Success("Review deleted successfully");
            }
            else
            {
                return ResponseHandler.BadRequest<string>("Failed to delete review");
            }
        }
        #endregion
    }
}
