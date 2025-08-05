using BuildingBlocks.Application.Cqrs.Commands;

namespace Administration.Application.Commands.DeleteReviewText
{
    public class DeleteReviewTextCommand : ICommand
    {
        public DeleteReviewTextCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}