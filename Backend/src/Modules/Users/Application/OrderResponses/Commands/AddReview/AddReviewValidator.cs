using FluentValidation;

namespace Users.Application.OrderResponses.Commands.AddReview
{
    internal class AddReviewValidator : AbstractValidator<AddReviewCommand>
    {
        public AddReviewValidator()
        {
            RuleFor(c => c.Text)
                .NotNull()
                .NotEmpty()
                .WithMessage("Text must be provided");
        }
    }
}