using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Images.Commands.Models
{
    public class DeleteImageCommand : IRequest<Response<string>>
    {
        public int ImageId { get; set; }

        public DeleteImageCommand(int imageId)
        {
            ImageId = imageId;
        }
    }
}
