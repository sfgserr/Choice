using Autofac;
using BuildingBlocks.Application.Cqrs.Commands;
using Identity.Infrastructure.Configuration;

namespace Identity.Infrastructure.Processing
{
    internal static class CommandsExecutor
    {
        internal static async Task ExecuteCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            using var scope = IdentityCompositionRoot.BeginLifetimeScope();

            var handlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));

            var handler = scope.Resolve(handlerType) as ICommandHandler<TCommand>;

            await handler!.Execute(command);
        }

        internal static async Task<TResult> ExecuteCommandAsync<TCommand, TResult>(TCommand command)
            where TCommand : ICommandWithResult<TResult>
        {
            using var scope = IdentityCompositionRoot.BeginLifetimeScope();

            var handlerType = typeof(ICommandHandlerWithResult<,>).MakeGenericType(typeof(TCommand), typeof(TResult));

            var handler = scope.Resolve(handlerType) as ICommandHandlerWithResult<TCommand, TResult>;

            return await handler!.Execute(command);
        }
    }
}