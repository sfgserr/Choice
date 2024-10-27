using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Chat.Application.Contracts;
using Chat.Domain.Messages;
using Chat.Domain.Messages.OrderMessages;

namespace Chat.Application.Messages.Commands.ChangeEnrollmentDate
{
    internal class ChangeEnrollmentDateCommandHandler : ICommandHandler<ChangeEnrollmentDateCommand>
    {
        private readonly IChatDbContext _dbContext;

        internal ChangeEnrollmentDateCommandHandler(IChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Execute(ChangeEnrollmentDateCommand command)
        {
            var orderMessage = await _dbContext.Messages.Get(o => 
                o.Type.Equals(MessageType.Order) && o.OrderMessage!.ResponseId.Equals(new OrderResponseId(command.ResponseId)));

            var newOrderMessage = Message.CreateOrder(
                orderMessage.OrderMessage!.ResponseId,
                command.EnrollmentDate,
                orderMessage.FromUserId,
                orderMessage.ToUserId);

            orderMessage.OrderMessage.SetAsInactive();

            await _dbContext.Messages.AddAsync(newOrderMessage);
        }
    }
}