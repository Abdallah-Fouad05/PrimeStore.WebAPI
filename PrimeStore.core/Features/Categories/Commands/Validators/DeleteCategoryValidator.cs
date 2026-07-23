using FluentValidation;
using PrimeStore.core.Features.Categories.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Categories.Commands.Validators
{
    internal class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        #region Fields
        private readonly ICategoryService _CategoryService;
        #endregion

        #region Constructors
        public DeleteCategoryValidator(ICategoryService categoryService)
        {
            ApplyValidationRules();
            _CategoryService = categoryService;
        }
        #endregion
        public void ApplyValidationRules()
        {
            RuleFor(x => x.CategoryId)
                    .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        }
    }
}