using FluentValidation;
using PrimeStore.core.Features.Users.Commands.Models;

namespace PrimeStore.core.Features.Users.Commands.Validators
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        #region Fields
        #endregion

        #region Constructor
        public DeleteUserValidator()
        {
            ApplyValidationRules();
        }
        #endregion

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} Must Be Greater than 0.");
        }
    }
}
