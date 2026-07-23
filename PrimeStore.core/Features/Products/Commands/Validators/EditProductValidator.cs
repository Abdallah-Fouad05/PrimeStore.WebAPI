using FluentValidation;
using PrimeStore.core.Features.Products.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Products.Commands.Valdatiors
{
    public class EditProductValidator : AbstractValidator<EditProductCommand>
    {
        #region Fields
        private readonly IProductService _ProductService;
        #endregion

        #region Constructors
        public EditProductValidator(IProductService productService)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _ProductService = productService;
        }

        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
                .NotNull().WithMessage("{PropertyValue} Must Not Be Null")
                .MaximumLength(500).WithMessage("Max Length is 500");

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
                    .MustAsync(async (x, title, cancellationToken) => !await _ProductService.IsTitleExist(title, x.ProductId))
                    .WithMessage("{PropertyName} Is Exist");
        }

        #endregion
    }

}
