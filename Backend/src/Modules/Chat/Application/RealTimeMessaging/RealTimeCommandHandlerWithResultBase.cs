using BuildingBlocks.Application.Cqrs.Commands;
using Chat.Application.Contracts;

namespace Chat.Application.RealTimeMessaging
{
    public abstract class RealTimeCommandHandlerWithResultBase<TCommand, TData> : 
        IRealTimeMessenger, ICommandHandlerWithResult<TCommand, TData> where TCommand : ICommandWithResult<TData>
    {
        private readonly IChatService _chatService;
        
        private TCommand _command;
        private TData _data;
        
        protected RealTimeCommandHandlerWithResultBase(IChatService chatService)
        {
            _chatService = chatService;
        }

        protected abstract Guid GetUserId(TCommand command);

        protected abstract Task<TData> HandleCommandAsync(TCommand command);

        public async Task<TData> Execute(TCommand command)
        {
            _command = command;
            
            _data = await HandleCommandAsync(command);
            
            return _data;
        }

        public async Task Send()
        {
            if (_data != null)
            {
                await _chatService.Send(_data, GetUserId(_command));
            }
        }
    }
}