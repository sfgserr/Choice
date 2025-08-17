using BuildingBlocks.Application.Cqrs.Commands;

namespace Administration.Application.Commands.BanUser
{
    public class BanUserCommand : ICommand
    {
        public BanUserCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}