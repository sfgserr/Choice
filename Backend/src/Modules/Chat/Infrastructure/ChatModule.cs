using Autofac;
using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Cqrs.Queries;
using Chat.Application.Contracts;
using Chat.Infrastructure.Configuration;
using Chat.Infrastructure.Processing;

namespace Chat.Infrastructure
{
    public class ChatModule : IChatModule
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
            using var scope = ChatCompositionRoot.BeginLifetimeScope();

            Type handlerType = typeof(IQueryHandler<,>).MakeGenericType(typeof(TQuery), typeof(TResult));

            var handler = scope.Resolve(handlerType) as IQueryHandler<TQuery, TResult>;

            return await handler!.Handle(query);
        }
    }
}