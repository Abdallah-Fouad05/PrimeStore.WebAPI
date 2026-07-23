using FluentValidation;
using PrimeStore.core.Features.Reviews.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Reviews.Commands.Validators
{
    public class EditReviewValidator : AbstractValidator<EditReviewCommand>
    {

        #region Fields
        private readonly IReviewService _ReviewService;
        #endregion

        #region Constructor
        public EditReviewValidator(IReviewService reviewService)
        {
            _ReviewService = reviewService;
            ApplyValidationRules();
        }
        #endregion

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
                .NotNull().WithMessage("{PropertyValue} Must Not Be Null");

            RuleFor(x => x.Rating)
                .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
                .NotNull().WithMessage("{PropertyValue} Must Not Be Null");


            RuleFor(x => x.Rating).GreaterThan(5).WithMessage("{PropertyName} Must Not Be Greater then 5")
                .LessThan(0).WithMessage("{PropertyName} Must Not Be Less than 0");


            RuleFor(x => x.ReviewId).LessThan(0).WithMessage("{PropertyName} Must Not Be Less than 0");

        }
    }
}
