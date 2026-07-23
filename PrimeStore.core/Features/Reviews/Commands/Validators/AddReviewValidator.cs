using FluentValidation;
using PrimeStore.core.Features.Reviews.Commands.Models;
using PrimeStore.service.Abstracts;

namespace PrimeStore.core.Features.Reviews.Commands.Validators
{
    public class AddReviewValidator : AbstractValidator<AddReviewCommand>
    {

        #region Fields
        private readonly IReviewService _ReviewService;
        #endregion

        #region Constructor
        public AddReviewValidator(IReviewService reviewService)
        {
            _ReviewService = reviewService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
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

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
                .NotNull().WithMessage("{PropertyValue} Must Not Be Null");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("{PropertyName} Must Not Be Empty")
                .NotNull().WithMessage("{PropertyValue} Must Not Be Null");

            RuleFor(x => x.Rating).GreaterThan(5).WithMessage("{PropertyName} Must Not Be Greater then 5")
                .LessThan(0).WithMessage("{PropertyName} Must Not Be Less than 0");


            RuleFor(x => x.ProductId).LessThan(0).WithMessage("{PropertyName} Must Not Be Less than 0");

            RuleFor(x => x.UserId).LessThan(0).WithMessage("{PropertyName} Must Not Be Less than 0");

        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.UserId)
                .MustAsync(async (x, Key, CancellationToken) => !await _ReviewService.IsReviewExist(Key, x.ProductId))
                .WithMessage("You have already reviewed this product.");
        }
    }
}
