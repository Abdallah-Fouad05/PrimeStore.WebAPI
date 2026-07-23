using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PrimeStore.core.Features.Users.Commands.Models;
using PrimeStore.data.Entities.Identity;

namespace PrimeStore.core.Features.Users.Commands.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly UserManager<User> _UserManager;
        #endregion
        #region Constructor
        public AddUserValidator(UserManager<User> userManager)
        {
            _UserManager = userManager;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        public void ApplyValidationRules()
        {
            RuleFor(x => x.FullName)
               .NotEmpty()
               .WithMessage("{PropertyName} is required.")
               .MaximumLength(500)
               .WithMessage("{PropertyName} Must not exceed 500 characters.");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(500)
                .WithMessage("{PropertyName} Must not exceed 500 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.");

            RuleFor(x => x.ImageUrl)
                .Must(uri => uri == null || (!string.IsNullOrWhiteSpace(uri) && Uri.IsWellFormedUriString(uri, UriKind.Absolute)))
                .WithMessage("Image URL must be a valid URL.");

            RuleFor(x => x.Password)
              .NotEmpty()
              .WithMessage("{PropertyName} is required.")
              .MaximumLength(50)
              .WithMessage("{PropertyName} Must not exceed 50");

            RuleFor(x => x.ConfirmPassword)
                .Matches(x => x.Password).WithMessage("ConfirmPassword Not Matches Password");

        }

        public void ApplyCustomValidationRules()
        {

        }
    }

}
