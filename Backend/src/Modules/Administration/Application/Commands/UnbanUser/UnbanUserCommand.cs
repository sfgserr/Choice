using BuildingBlocks.Application.Cqrs.Commands;

namespace Administration.Application.Commands.UnbanUser
{
    public class UnbanUserCommand : ICommand
    {
        public UnbanUserCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}