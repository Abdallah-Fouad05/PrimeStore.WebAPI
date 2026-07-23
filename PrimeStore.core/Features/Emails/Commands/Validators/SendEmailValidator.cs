using FluentValidation;
using SchoolProject.Core.Features.Emails.Commands.Models;

namespace SchoolProject.Core.Features.Emails.Commands.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        #region Constructors
        public SendEmailValidator()
        {

            ApplyValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} Must Not be Empty")
                .NotNull().WithMessage("{PropertyName} Must Not be Null")
                .EmailAddress().WithMessage("{PropertyName} InValid");

            RuleFor(x => x.Message)
                      .NotEmpty().WithMessage("{PropertyName} Must Not be Empty")
                      .NotNull().WithMessage("{PropertyName} Must Not be Null");
        }
        #endregion
    }
}
