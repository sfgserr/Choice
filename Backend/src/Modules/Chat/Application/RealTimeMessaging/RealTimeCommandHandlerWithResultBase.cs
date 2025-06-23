using BuildingBlocks.Application.Cqrs.Commands;
using Chat.Application.Contracts;

namespace Chat.Application.RealTimeMessaging
{
    internal abstract class RealTimeCommandHandlerWithResultBase<TCommand, TData> : 
        RealTimeMessengerBase<TCommand, TData>, ICommandHandlerWithResult<TCommand, TData> where TCommand : ICommandWithResult<TData>
    {
        protected RealTimeCommandHandlerWithResultBase(
            IChatService chatService,
            string methodName) : base(chatService, methodName)
        {
        }

        public async Task<TData> Execute(TCommand command)
        {
            return await SetCommandAndData(command);
        }
    }
}