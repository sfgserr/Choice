namespace BuildingBlocks.Application.Cqrs.Commands
{
    public interface ICommandHandlerWithResult<TCommand, TResult> where TCommand : ICommandWithResult<TResult>
    {
        Task<TResult> Execute(TCommand command);
    }
}