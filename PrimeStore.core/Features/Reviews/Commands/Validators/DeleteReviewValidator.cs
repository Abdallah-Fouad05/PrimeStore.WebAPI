using FluentValidation;
using PrimeStore.core.Features.Reviews.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Reviews.Commands.Validators
{
    public class DeleteReviewValidator : AbstractValidator<DeleteReviewCommand>
    {
        #region Fields
        private readonly IReviewService _ReviewService;
        #endregion

        #region Constructor
        public DeleteReviewValidator(IReviewService reviewService)
        {
            _ReviewService = reviewService;
            ApplyValidationRules();
        }
        #endregion

        public void ApplyValidationRules()
        {
            RuleFor(x => x.ReviewId).LessThan(0).WithMessage("{PropertyName} Must Not Be Less than 0");
        }
    }
}
