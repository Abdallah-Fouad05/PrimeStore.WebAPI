using FluentValidation;
using PrimeStore.core.Features.Wishlist.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Wishlist.Commands.Validators
{
    public class AddWishlistValidator : AbstractValidator<AddWishlistCommand>
    {
        #region Fields
        private readonly IWishlistService _WishlistService;
        #endregion
        #region Constructor
        public AddWishlistValidator(IWishlistService wishlistService)
        {
            _WishlistService = wishlistService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        public void ApplyValidationRules()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.ProductId)
                .MustAsync(async (x, ProductId, CancellationToken) => !await _WishlistService.HasUserWishlistedProductAsync(x.UserId, ProductId))
                .WithMessage("Product Is Already in your Wishlist");
        }

    }
}
