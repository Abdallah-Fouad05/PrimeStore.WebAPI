using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Reviews.Commands.Models
{
    public class DeleteReviewCommand : IRequest<Response<string>>
    {
        public int ReviewId { get; set; }

        public DeleteReviewCommand(int reviewId)
        {
            ReviewId = reviewId;
        }
    }
}
