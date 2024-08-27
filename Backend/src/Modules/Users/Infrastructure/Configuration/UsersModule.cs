using Autofac;
using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Cqrs.Queries;
using Users.Application.Contracts;
using Users.Infrastructure.Processing;

namespace Users.Infrastructure.Configuration
{
    internal class UsersModule : IUsersModule
    {
        public async Task ExecuteCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            await CommandsExecutor.ExecuteCommandAsync(command);
        }

        public async Task<TResult> ExecuteCommand<TCommand, TResult>(TCommand command) where TCommand : ICommandWithResult<TResult>
        {
            return await CommandsExecutor.ExecuteCommandAsync<TCommand, TResult>(command);
        }

        public async Task<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            using var scope = UsersCompositionRoot.BeginLifetimeScope();

            Type handlerType = typeof(IQueryHandler<,>).MakeGenericType(typeof(TQuery), typeof(TResult));

            var handler = scope.Resolve(handlerType) as IQueryHandler<TQuery, TResult>;

            return await handler!.Handle(query);
        }
    }
}
