using BuildingBlocks.Application.Extensions;
using Chat.Application.Contracts;
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
            
            return addedMessage.Entity.ToDto<MessageDto>();
        }

        protected override Guid GetUserId(CreateOrderMessageCommand command)
        {
            return command.ToUserId;
        }
    }
}