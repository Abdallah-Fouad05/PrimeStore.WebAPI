using FluentValidation;
using PrimeStore.core.Features.Brands.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Brands.Commands.Validators
{
    public class DeleteBrandValidator : AbstractValidator<DeleteBrandCommand>
    {
        #region Feilds
        private readonly IBrandService _BrandService;
        #endregion

        #region Constructor
        public DeleteBrandValidator(IBrandService brandService)
        {
            _BrandService = brandService;
            ApplyValidationRules();
        }
        #endregion

        public void ApplyValidationRules()
        {
            RuleFor(x => x.BrandId).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        }
    }
}
