using FluentValidation;
using PrimeStore.core.Features.Brands.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Brands.Commands.Validators
{
    public class AddBrandValidator : AbstractValidator<AddBrandCommand>
    {
        #region Fields
        private readonly IBrandService _BrandService;
        #endregion

        #region Constructors
        public AddBrandValidator(IBrandService brandService)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _BrandService = brandService;
        }
        #endregion
        public void ApplyValidationRules()
        {
            RuleFor(x => x.BrandName).NotEmpty().WithMessage("{PropertyName} Must be Not Empty");

            RuleFor(x => x.BrandName).Must(x => !string.IsNullOrWhiteSpace(x))
                                     .WithMessage("{PropertyName} Must be Not Empty");

            RuleFor(x => x.BrandName).NotNull().WithMessage("{PropertyName} Must be Not Null");

            RuleFor(x => x.ImageUrl)
                .Must(uri => uri == null || (!string.IsNullOrWhiteSpace(uri) && Uri.IsWellFormedUriString(uri, UriKind.Absolute)))
                .WithMessage("Image URL must be a valid URL.");
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.BrandName)
                .MustAsync(async (key, CancellationToken) => !await _BrandService.IsBrandNameExist(key))
                .WithMessage("Brand Name Is Already Exist");
        }

    }
}
