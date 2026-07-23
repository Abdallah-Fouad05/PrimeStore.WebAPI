using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Categories.Queries.Models;
using PrimeStore.core.Features.Categories.Queries.Results;
using PrimeStore.Core.Bases;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Categories.Queries.Handlers
{
    public class CategoryHandler : IRequestHandler<GetCategoriesListQuery, Response<List<GetCategoriesListResponse>>>
    {

        private readonly ICategoryService _categoryService;
        private readonly IMapper _Mapper;

        public CategoryHandler(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _Mapper = mapper;
        }

        private List<Category> BuildTree(List<Category> categories, int? parentId = null)
        {
            return categories
                .Where(x => x.ParentCategoryID == parentId)
                .Select(x =>
                {
                    x.ChildCategories = BuildTree(categories, x.CategoryId);
                    return x;
                })
                .ToList();
        }

        public async Task<Response<List<GetCategoriesListResponse>>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var Categories = await _categoryService.GetCategoriesListAsync();

            var CategoriesTree = BuildTree(Categories);

            var CategoriesMapping = _Mapper.Map<List<GetCategoriesListResponse>>(CategoriesTree);

            var Result = ResponseHandler.Success(CategoriesMapping);

            Result.Meta = new { Count = CategoriesMapping.Count() };

            return Result;
        }

    }
}
