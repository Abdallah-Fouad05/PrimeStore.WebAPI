using FluentValidation;
using PrimeStore.core.Features.Authenication.Commands.Models;

namespace PrimeStore.core.Features.Authentication.Commands.Validators
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
        {
            ApplyValidation();
        }

        public void ApplyValidation()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");


            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

        }
    }
}
