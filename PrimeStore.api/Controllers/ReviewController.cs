using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeStore.Api.Base;
using PrimeStore.core.Features.Reviews.Commands.Models;
using PrimeStore.core.Features.Reviews.Queries.Models;
using PrimeStore.core.Features.Reviews.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.Core.Wrappers;
using static PrimeStore.Data.AppMetaData.Router;

namespace PrimeStore.api.Controllers
{
    public class ReviewController : AppControllerBase
    {
        [HttpGet(ReviewRouting.ProductReviewsPaginated)]
        [ProducesResponseType(typeof(PaginatedResult<GetProductReviewsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductReviewsPaginated(GetProductReviewsQuery request)
        {
            var response = _Mediator.Send(request);
            return Ok(response);
        }


        [Authorize]
        [HttpGet(ReviewRouting.UserReviewsPaginated)]
        [ProducesResponseType(typeof(PaginatedResult<GetUserReviewsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserReviewsPaginated(GetUserReviewsQuery request)
        {
            var response = _Mediator.Send(request);
            return Ok(response);
        }

        [Authorize]
        [HttpPost(ReviewRouting.Create)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddReview([FromBody] AddReviewCommand request)
        {
            var response = await _Mediator.Send(request);

            return NewResult(response);
        }

        [Authorize]
        [HttpPut(ReviewRouting.Edit)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditReview([FromBody] EditReviewCommand request)
        {
            var response = await _Mediator.Send(request);

            return NewResult(response);
        }

        [Authorize]
        [HttpDelete(ReviewRouting.Delete)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteReview([FromQuery] int BrandId)
        {
            var response = await _Mediator.Send(new DeleteReviewCommand(BrandId));

            return NewResult(response);
        }

    }
}
