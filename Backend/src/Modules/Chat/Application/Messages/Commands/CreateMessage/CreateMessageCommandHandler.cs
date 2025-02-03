using BuildingBlocks.Application.Cqrs.Commands;
using Chat.Application.Contracts;
using Chat.Domain.Messages;
using Chat.Domain.ChatUsers;

namespace Chat.Application.Messages.Commands.CreateMessage
{
    internal class CreateMessageCommandHandler : ICommandHandlerWithResult<CreateMessageCommand, MessageDto>
    {
        private readonly IChatDbContext _dbContext;
        private readonly IUserContext _userContext;
        
        internal CreateMessageCommandHandler(
            IChatDbContext dbContext, 
            IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<MessageDto> Execute(CreateMessageCommand command)
        {
            var message = Message.CreateMessage(
                command.Content,
                _userContext.Id,
                new(command.ToUserId),
                MessageType.Parse(command.Type));
            
            var addedMessage = await _dbContext.Messages.AddAsync(message);

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
    }
}
