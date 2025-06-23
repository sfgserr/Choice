using BuildingBlocks.Application.Extensions;
using Chat.Application.Contracts;
using Chat.Application.RealTimeMessaging;
using Chat.Domain.Messages;
using Chat.Domain.ChatUsers;

namespace Chat.Application.Messages.Commands.CreateMessage
{
    internal class CreateMessageCommandHandler : 
        RealTimeCommandHandlerWithResultBase<CreateMessageCommand, MessageDto>, INotifiableCommandHandler<CreateMessageCommand>
    {
        private readonly IChatDbContext _dbContext;
        private readonly IUserContext _userContext;
        
        internal CreateMessageCommandHandler(
            IChatDbContext dbContext, 
            IUserContext userContext, 
            IChatService chatService) : base(chatService, "messageSent")
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        protected override async Task<MessageDto> HandleCommandAsync(CreateMessageCommand command)
        {
            var message = Message.CreateMessage(
                command.Content,
                _userContext.Id,
                new(command.ToUserId),
                MessageType.Parse(command.Type));
            
            var addedMessage = await _dbContext.Messages.AddAsync(message);
            
            return addedMessage.Entity.ToDto<MessageDto>();
        }

        protected override Guid GetUserId(CreateMessageCommand command)
        {
            return command.ToUserId;
        }

        public Notification GetNotification(CreateMessageCommand command)
        {
            return new Notification(command.ToUserId, "Новое сообщение", command.Content);
        }
    }
}
