using BuildingBlocks.Application.Cqrs.Commands;

namespace Administration.Application.Commands.EditReview
{
    public class EditReviewCommand : ICommand 
    {
        public EditReviewCommand(Guid reviewId, string text, int grade)
        {
            ReviewId = reviewId;
            Text = text;
            Grade = grade;
        }

        public Guid ReviewId { get; }

        public string Text { get; }

        public int Grade { get; }
    }
}