using FluentValidation;
using PrimeStore.core.Features.Products.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Products.Commands.Validators
{
    public class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        #region Fields
        private readonly IProductService _ProductService;
        #endregion

        #region Constructors
        public AddProductValidator(IProductService productService)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _ProductService = productService;
        }

        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
                .NotNull().WithMessage("{PropertyValue} Must Not Be Null")
                .MaximumLength(500).WithMessage("Max Length is 500");

            RuleFor(x => x.Title).Must(x => !string.IsNullOrWhiteSpace(x))
                              .WithMessage("{PropertyName} Must be Not Empty");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(x => x.BrandId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(x => x.Stock)
                .GreaterThan(-1).WithMessage("{PropertyName} must be greater than or equal to 0");

            RuleForEach(x => x.ProductImage)
                .ChildRules(image =>
                {
                    image.RuleFor(x => x.ImageUrl)
                        .Must(uri =>
                            uri == null ||
                            (!string.IsNullOrWhiteSpace(uri) &&
                             Uri.IsWellFormedUriString(uri, UriKind.Absolute)))
                        .WithMessage("Image URL must be a valid URL.");
                });
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Title)
                .MustAsync(async (Key, CancellationToken) => !await _ProductService.IsTitleExist(Key))
                .WithMessage("{PropertyName} Is Exist");


        }
        #endregion

    }
}
