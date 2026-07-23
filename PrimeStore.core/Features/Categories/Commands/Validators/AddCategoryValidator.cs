using FluentValidation;
using PrimeStore.core.Features.Categories.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Categories.Commands.Validators
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {
        #region Fields
        private readonly ICategoryService _CategoryService;
        #endregion

        #region Constructors
        public AddCategoryValidator(ICategoryService categoryService)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _CategoryService = categoryService;
        }
        #endregion
        public void ApplyValidationRules()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("{PropertyName} Must be Not Empty");

            RuleFor(x => x.CategoryName).Must(x => !string.IsNullOrWhiteSpace(x))
                                     .WithMessage("{PropertyName} Must be Not Empty");

            RuleFor(x => x.CategoryName).NotNull().WithMessage("{PropertyName} Must be Not Null");
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.CategoryName)
                .MustAsync(async (key, CancellationToken) => !await _CategoryService.IsCategoryNameExist(key))
                .WithMessage("Category Name Is Already Exist");
        }
    }
}
