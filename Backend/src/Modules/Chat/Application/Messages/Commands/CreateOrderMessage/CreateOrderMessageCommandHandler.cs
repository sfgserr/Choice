using BuildingBlocks.Application.Cqrs.Commands;
using Chat.Application.Contracts;
using Chat.Domain.Messages;

namespace Chat.Application.Messages.Commands.CreateOrderMessage
{
    internal class CreateOrderMessageCommandHandler : ICommandHandler<CreateOrderMessageCommand>
    {
        private readonly IChatDbContext _dbContext;
        
        internal CreateOrderMessageCommandHandler(IChatDbContext dbContext, IChatService chatService)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(CreateOrderMessageCommand command)
        {
            var orderMessage = Message.CreateOrder(
                new(command.ResponseId),
                null,
                new(command.FromUserId),
                new(command.ToUserId));
            
            await _dbContext.Messages.AddAsync(orderMessage);
        }
    }
}