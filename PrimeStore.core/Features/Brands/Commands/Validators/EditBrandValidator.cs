using FluentValidation;
using PrimeStore.core.Features.Brands.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Brands.Commands.Validators
{
    public class EditBrandValidator : AbstractValidator<EditBrandCommand>
    {
        #region Feilds
        private readonly IBrandService _BrandService;
        #endregion

        #region Constructor
        public EditBrandValidator(IBrandService brandService)
        {
            _BrandService = brandService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        public void ApplyValidationRules()
        {
            RuleFor(x => x.BrandId)
               .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

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
                .MustAsync(async (x, key, CancellationToken) => !await _BrandService.IsBrandNameExist(key, x.BrandId))
                .WithMessage("Brand Name Is Already Exist");
        }
    }
}
