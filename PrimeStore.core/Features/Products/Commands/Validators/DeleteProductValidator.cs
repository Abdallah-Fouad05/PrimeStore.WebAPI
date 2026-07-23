using FluentValidation;
using PrimeStore.core.Features.Products.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Products.Commands.Validators
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        #region Fields
        private readonly IProductService _ProductService;
        #endregion

        #region Constructors
        public DeleteProductValidator(IProductService productService)
        {
            ApplyValidationRules();
            _ProductService = productService;
        }

        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        }

        #endregion
    }
}
