using FluentValidation;
using PrimeStore.core.Features.Wishlist.Queries.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Wishlist.Queries.Validators
{
    internal class IsProductInUserWishlistValidator : AbstractValidator<IsProductInUserWishlistQuery>
    {
        #region Fields
        private readonly IWishlistService _WishlistService;
        #endregion
        #region Constructor
        public IsProductInUserWishlistValidator(IWishlistService wishlistService)
        {
            _WishlistService = wishlistService;
            ApplyValidationRules();
        }
        #endregion

        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
