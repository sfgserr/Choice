using BuildingBlocks.Application.Cqrs.Commands;
using Chat.Application.Contracts;

namespace Chat.Application.Chat.Commands.SendOrderMessage
{
    internal class SendOrderMessageCommandHandler : ICommandHandler<SendOrderMessageCommand>
    {
        private readonly IChatService _chatService;
        
        internal SendOrderMessageCommandHandler(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task Execute(SendOrderMessageCommand command)
        {
            await _chatService.Send(command.Data, command.ToUserId);
        }
    }
}