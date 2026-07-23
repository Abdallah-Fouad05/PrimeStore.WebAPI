using AutoMapper;
using MediatR;
using PrimeStore.core.Features.Categories.Commands.Models;
using PrimeStore.Core.Bases;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Categories.Commands.Handlers
{
    public class CategoryHandler :
        IRequestHandler<AddCategoryCommand, Response<string>>,
        IRequestHandler<EditCategoryCommand, Response<string>>,
        IRequestHandler<DeleteCategoryCommand, Response<string>>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryHandler(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);

            var result = await _categoryService.AddAsync(category);

            return ResponseHandler.Created(result);
        }

        public async Task<Response<string>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetByIDAsync(request.CategoryId);

            if (category == null)
                return ResponseHandler.NotFound<string>("Category not found");

            _mapper.Map(request, category);

            var result = await _categoryService.UpdateAsync(category);

            return ResponseHandler.Success(result);
        }

        public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetByIDAsync(request.CategoryId);

            if (category == null)
                return ResponseHandler.NotFound<string>("Category not found");

            var result = await _categoryService.DeleteAsync(category);

            return ResponseHandler.Success(result);
        }
    }
}
