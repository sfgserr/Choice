using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Chat.Application.Contracts;
using Chat.Domain.Messages;
using Users.Application.Contracts;
using Users.Application.OrderResponses.Queries.GetOrderResponse;

namespace Chat.Application.Chat.Commands.SendMessageCommand
{
    internal class SendMessageCommandHandler : ICommandHandler<SendMessageCommand>
    {
        private readonly IChatDbContext _dbContext;
        private readonly IChatService _chatService;
        
        internal SendMessageCommandHandler(IChatDbContext dbContext, IChatService chatService)
        {
            _dbContext = dbContext;
            _chatService = chatService;
        }

        public async Task Execute(SendMessageCommand command)
        {
            var message = await _dbContext.Messages.GetAsNoTracking(m => 
                m.Id.Equals(new MessageId(command.MessageId)));
            
            var dto = new MessageDto(
                message.Id.Value,
                message.FromUserId.Value,
                message.ToUserId.Value,
                message.Body,
                message.IsRead,
                message.Type.Value,
                message.OrderMessage?.ResponseId.Value,
                message.CreationDate,
                message.OrderMessage?.EnrollmentDate,
                message.OrderMessage?.IsActive);

            await _chatService.SendMessage(dto);
        }
    }
}