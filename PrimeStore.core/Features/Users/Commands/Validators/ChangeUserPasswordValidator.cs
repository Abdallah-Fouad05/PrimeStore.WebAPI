using FluentValidation;
using PrimeStore.core.Features.Users.Commands.Models;

namespace PrimeStore.core.Features.Users.Commands.Validators
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        #region Fields
        #endregion

        #region Constructor
        public ChangeUserPasswordValidator()
        {
            ApplyValidationRules();
        }
        #endregion

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} Must Be Greater than 0.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(50)
                .WithMessage("{PropertyName} Must not exceed 50");


            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(50)
                .WithMessage("{PropertyName} Must not exceed 50");


            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MaximumLength(50)
                .WithMessage("{PropertyName} Must not exceed 50");

            RuleFor(x => x.ConfirmPassword)
                .Matches(x => x.NewPassword).WithMessage("Confirm Password Not Matches New Password");

        }
    }
}
