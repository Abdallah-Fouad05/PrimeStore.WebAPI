using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Categories.Commands.Models
{
    public class DeleteCategoryCommand : IRequest<Response<string>>
    {
        public int CategoryId { get; set; }

        public DeleteCategoryCommand(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
