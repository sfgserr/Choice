using BuildingBlocks.Application.Cqrs.Commands;
using Chat.Application.Contracts;
using Chat.Domain.Messages;
using Chat.Domain.ChatUsers;

namespace Chat.Application.Messages.Commands.CreateMessage
{
    internal class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand>
    {
        private readonly IChatDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IChatService _chatService;
        
        internal CreateMessageCommandHandler(
            IChatDbContext dbContext, 
            IUserContext userContext, 
            IChatService chatService)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _chatService = chatService;
        }

        public async Task Execute(CreateMessageCommand command)
        {
            var message = Message.CreateMessage(
                command.Content,
                _userContext.Id,
                new(command.ToUserId),
                MessageType.Parse(command.Type));

            await _chatService.SendMessage(message);
            
            await _dbContext.Messages.AddAsync(message);
        }
    }
}
