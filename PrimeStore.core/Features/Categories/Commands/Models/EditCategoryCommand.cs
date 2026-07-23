using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Categories.Commands.Models
{
    public class EditCategoryCommand : IRequest<Response<string>>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public int? ParentCategoryID { get; set; }

    }
}
