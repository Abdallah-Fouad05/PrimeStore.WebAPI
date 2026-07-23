using MediatR;
using Microsoft.AspNetCore.Http;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Images.Commands.Models
{
    public class AddImageCommand : IRequest<Response<string>>
    {

        public required IFormFile Image;

    }
}
