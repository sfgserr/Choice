using BuildingBlocks.Application.Cqrs.Commands;

namespace Administration.Application.Commands.AddReviewText
{
    public class AddReviewTextCommand : ICommand
    {
        public AddReviewTextCommand(int grade, string text)
        {
            Grade = grade;
            Text = text;
        }

        public int Grade { get; }

        public string Text { get; }
    }
}