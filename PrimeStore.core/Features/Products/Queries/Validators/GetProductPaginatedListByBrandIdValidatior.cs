using FluentValidation;
using PrimeStore.core.Features.Products.Queries.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Products.Queries.Validators
{
    public class GetProductPaginatedListByBrandIdValidator : AbstractValidator<GetProductPaginatedListByBrandIdQuery>
    {
        #region Fields
        private readonly IBrandService _BrandService;
        #endregion

        #region Constructors
        public GetProductPaginatedListByBrandIdValidator(IBrandService brandService)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _BrandService = brandService;
        }

        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.BrandId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.BrandId)
                .MustAsync(async (Key, CancellationToken) => await _BrandService.IsBrandExist(Key))
                .WithMessage("{PropertyName} Is Not Exist");
        }
        #endregion
    }
}
