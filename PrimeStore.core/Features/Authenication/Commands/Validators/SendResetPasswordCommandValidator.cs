using FluentValidation;
using PrimeStore.core.Features.Authenication.Commands.Models;


namespace PrimeStore.Core.Features.Authentication.Commands.Validators
{
    public class SendResetPasswordCommandValidator : AbstractValidator<SendResetPasswordCodeCommand>
    {
        #region Fields
        #endregion

        #region Constructors
        public SendResetPasswordCommandValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email Must Be Not Empty")
                 .NotNull().WithMessage("Email Must Be Not Null")
                 .EmailAddress().WithMessage("Valid Email");


        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion

    }
}