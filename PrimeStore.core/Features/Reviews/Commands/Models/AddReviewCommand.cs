using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Reviews.Commands.Models
{
    public class AddReviewCommand : IRequest<Response<string>>
    {
        public required string Comment { get; set; }
        public float Rating { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
