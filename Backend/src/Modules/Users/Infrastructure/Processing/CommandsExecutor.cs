using Autofac;
using BuildingBlocks.Application.Cqrs.Commands;
using Users.Infrastructure.Configuration;

namespace Users.Infrastructure.Processing
{
    internal static class CommandsExecutor
    {
        internal async static Task ExecuteCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            using var scope = UsersCompositionRoot.BeginLifetimeScope();

            Type handlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));

            var handler = scope.Resolve(handlerType) as ICommandHandler<TCommand>;

            await handler!.Execute(command);
            return;
        }

        internal async static Task<TResult> ExecuteCommandAsync<TCommand, TResult>(TCommand command)
            where TCommand : ICommandWithResult<TResult>
        {
            using var scope = UsersCompositionRoot.BeginLifetimeScope();

            Type handlerType = typeof(ICommandHandlerWithResult<,>).MakeGenericType(typeof(TCommand), typeof(TResult));

            var handler = scope.Resolve(handlerType) as ICommandHandlerWithResult<TCommand, TResult>;

            return await handler!.Execute(command);
        }
    }
}
