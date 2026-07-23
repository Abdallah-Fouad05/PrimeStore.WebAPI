using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Reviews.Commands.Models
{
    public class EditReviewCommand : IRequest<Response<string>>
    {
        public int ReviewId { get; set; }
        public required string Comment { get; set; }
        public float Rating { get; set; }
    }
}
