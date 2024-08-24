using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Cqrs.Queries;

namespace Users.Application.Contracts
{
    public interface IUsersModule
    {
        Task ExecuteCommand<TCommand>(TCommand command) where TCommand : ICommand;

        Task<TResult> ExecuteCommand<TCommand, TResult>(TCommand command) where TCommand : ICommandWithResult<TResult>;

        Task<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
