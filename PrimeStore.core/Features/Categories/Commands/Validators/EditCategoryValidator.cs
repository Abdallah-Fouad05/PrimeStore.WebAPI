using FluentValidation;
using PrimeStore.core.Features.Categories.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Categories.Commands.Validators
{
    public class EditCategoryValidator : AbstractValidator<EditCategoryCommand>
    {
        #region Fields
        private readonly ICategoryService _CategoryService;
        #endregion

        #region Constructors
        public EditCategoryValidator(ICategoryService categoryService)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _CategoryService = categoryService;
        }
        #endregion
        public void ApplyValidationRules()
        {
            RuleFor(x => x.CategoryId)
                    .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("{PropertyName} Must be Not Empty");

            RuleFor(x => x.CategoryName).Must(x => !string.IsNullOrWhiteSpace(x))
                                     .WithMessage("{PropertyName} Must be Not Empty");

            RuleFor(x => x.CategoryName).NotNull().WithMessage("{PropertyName} Must be Not Null");
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.CategoryName)
                .MustAsync(async (x, key, CancellationToken) => !await _CategoryService.IsCategoryNameExist(key, x.CategoryId))
                .WithMessage("Category Name Is Already Exist");
        }
    }
}
