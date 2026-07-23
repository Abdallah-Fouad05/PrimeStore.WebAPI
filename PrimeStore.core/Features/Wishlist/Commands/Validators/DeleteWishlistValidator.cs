using FluentValidation;
using PrimeStore.core.Features.Wishlist.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Wishlist.Commands.Validators
{
    public class DeleteWishlistValidator : AbstractValidator<DeleteWishlistCommand>
    {
        #region Fields
        private readonly IWishlistService _WishlistService;
        #endregion
        #region Constructor
        public DeleteWishlistValidator(IWishlistService wishlistService)
        {
            _WishlistService = wishlistService;
            ApplyValidationRules();
        }
        #endregion

        public void ApplyValidationRules()
        {
            RuleFor(x => x.WishlistId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }

    }
}
