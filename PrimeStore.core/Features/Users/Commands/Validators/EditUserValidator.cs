using FluentValidation;
using PrimeStore.core.Features.Users.Commands.Models;

namespace PrimeStore.core.Features.Users.Commands.Validators
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        #region Fields
        #endregion

        #region Constructor
        public EditUserValidator()
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} Must Be Greater than 0.");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(500)
                .WithMessage("{PropertyName} Must not exceed 500 characters.");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(500)
                .WithMessage("{PropertyName} Must not exceed 500 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.");


            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.");


            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.");

            RuleFor(x => x.ImageUrl)
                .Must(uri => uri == null || (!string.IsNullOrWhiteSpace(uri) && Uri.IsWellFormedUriString(uri, UriKind.Absolute)))
                .WithMessage("Image URL must be a valid URL.");

        }

        public void ApplyCustomValidationRules()
        {

        }
    }
}
