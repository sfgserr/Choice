using Chat.Application.Contracts;
using Chat.Application.Messages.Commands.CreateMessage;
using Chat.Application.RealTimeMessaging;
using Chat.Domain.Messages;

namespace Chat.Application.Messages.Commands.CreateOrderMessage
{
    internal class CreateOrderMessageCommandHandler : RealTimeCommandHandlerBase<CreateOrderMessageCommand, MessageDto>
    {
        private readonly IChatDbContext _dbContext;
        
        internal CreateOrderMessageCommandHandler(IChatDbContext dbContext, IChatService chatService) : base(chatService, "messageSent")
        {
            _dbContext = dbContext;
        }

        protected override async Task<MessageDto> HandleCommandAsync(CreateOrderMessageCommand command)
        {
            var orderMessage = Message.CreateOrder(
                new(command.ResponseId),
                null,
                new(command.FromUserId),
                new(command.ToUserId));
            
            var addedMessage = await _dbContext.Messages.AddAsync(orderMessage);
            
            return new MessageDto(
                addedMessage.Entity.Id.Value,
                addedMessage.Entity.FromUserId.Value,
                addedMessage.Entity.Body,
                addedMessage.Entity.IsRead,
                addedMessage.Entity.Type.Value,
                addedMessage.Entity.OrderMessage?.ResponseId.Value,
                addedMessage.Entity.CreationDate,
                addedMessage.Entity.OrderMessage?.EnrollmentDate,
                addedMessage.Entity.OrderMessage?.IsActive);
        }

        protected override Guid GetUserId(CreateOrderMessageCommand command)
        {
            return command.ToUserId;
        }
    }
}