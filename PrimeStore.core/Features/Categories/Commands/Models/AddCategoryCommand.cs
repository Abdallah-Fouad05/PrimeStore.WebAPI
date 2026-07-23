using MediatR;
using PrimeStore.Core.Bases;

namespace PrimeStore.core.Features.Categories.Commands.Models
{
    public class AddCategoryCommand : IRequest<Response<string>>
    {
        public string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public int? ParentCategoryID { get; set; }

    }
}
