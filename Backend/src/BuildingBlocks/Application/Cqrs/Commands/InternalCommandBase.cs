
namespace BuildingBlocks.Application.Cqrs.Commands
{
    public abstract class InternalCommandBase : ICommand
    {
        public InternalCommandBase(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
