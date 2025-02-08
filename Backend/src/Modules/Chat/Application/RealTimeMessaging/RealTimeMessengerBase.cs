using Chat.Application.Contracts;

namespace Chat.Application.RealTimeMessaging
{
    internal abstract class RealTimeMessengerBase<TCommand, TData> : IRealTimeMessenger
    {
        private readonly IChatService _chatService;
        private readonly string _methodName;
        
        private TCommand _command;
        private TData _data;
        
        protected RealTimeMessengerBase(IChatService chatService, string methodName)
        {
            _chatService = chatService;
            _methodName = methodName;
        }

        protected abstract Guid GetUserId(TCommand command);

        protected abstract Task<TData> HandleCommandAsync(TCommand command);

        protected async Task<TData> SetCommandAndData(TCommand command)
        {
            _command = command;
            
            _data = await HandleCommandAsync(command);
            
            return _data;
        }

        public async Task Send()
        {
            if (_data != null)
            {
                await _chatService.Send(_data, GetUserId(_command), _methodName);
            }
        }
    }
}