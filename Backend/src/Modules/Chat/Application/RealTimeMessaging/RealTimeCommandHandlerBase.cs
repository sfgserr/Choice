using BuildingBlocks.Application.Cqrs.Commands;
using Chat.Application.Contracts;

namespace Chat.Application.RealTimeMessaging
{
    internal abstract class RealTimeCommandHandlerBase<TCommand, TData> : 
        RealTimeMessengerBase<TCommand, TData>, ICommandHandler<TCommand> where TCommand : ICommand
    {
        protected RealTimeCommandHandlerBase(IChatService chatService, string methodName) : base(chatService, methodName)
        {
        }

        public async Task Execute(TCommand command)
        {
            await SetCommandAndData(command);
        }
    }
}