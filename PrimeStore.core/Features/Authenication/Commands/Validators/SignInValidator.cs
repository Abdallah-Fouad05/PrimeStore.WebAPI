using FluentValidation;
using PrimeStore.core.Features.Authentication.Commands.Models;

namespace PrimeStore.core.Features.Authentication.Commands.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator()
        {
            ApplyValidation();
        }

        public void ApplyValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(50)
                .WithMessage("{PropertyName} Must not exceed 50");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(50)
                .WithMessage("{PropertyName} Must not exceed 50");
        }
    }
}
