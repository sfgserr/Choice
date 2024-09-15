using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderResponses.Commands.AddReview
{
    public class AddReviewCommand : ICommand
    {
        public AddReviewCommand(Guid responseId, Guid toUserId, string text, int grade)
        {
            ResponseId = responseId;
            ToUserId = toUserId;
            Text = text;
            Grade = grade;
        }
        
        public Guid ResponseId { get; }
        
        public Guid ToUserId { get; }

        public string Text { get; }
        
        public int Grade { get; }
    }
}