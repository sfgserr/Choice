namespace BuildingBlocks.Application.Cqrs.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task Execute(TCommand command);
    }
}
