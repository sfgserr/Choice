using Chat.Application.Contracts;
using Chat.Application.Messages.Commands.CreateMessage;
using Chat.Application.RealTimeMessaging;
using Chat.Domain.Messages;
using Chat.Domain.Messages.OrderMessages;
using Microsoft.EntityFrameworkCore;

namespace Chat.Application.Messages.Commands.ChangeEnrollmentDate
{
    internal class ChangeEnrollmentDateCommandHandler : RealTimeCommandHandlerBase<ChangeEnrollmentDateCommand, MessageDto>
    {
        private readonly IChatDbContext _dbContext;

        internal ChangeEnrollmentDateCommandHandler(IChatService chatService, IChatDbContext dbContext) : base(chatService, "messageSent")
        {
            _dbContext = dbContext;
        }

        protected override async Task<MessageDto> HandleCommandAsync(ChangeEnrollmentDateCommand command)
        {
            var orderMessage = await _dbContext.Messages
                .Where(m => m.Type.Equals(MessageType.Order) && m.OrderMessage!.ResponseId.Equals(new OrderResponseId(
                    command.ResponseId)))
                .OrderByDescending(m => m.CreationDate)
                .FirstOrDefaultAsync();

            var newOrderMessage = Message.CreateOrder(
                orderMessage!.OrderMessage!.ResponseId,
                command.EnrollmentDate.ToUniversalTime(),
                orderMessage.ToUserId,
                orderMessage.FromUserId);

            orderMessage.OrderMessage.SetAsInactive();

            var message = await _dbContext.Messages.AddAsync(newOrderMessage);

            return new MessageDto(
                message.Entity.Id.Value,
                message.Entity.FromUserId.Value,
                message.Entity.Body,
                message.Entity.IsRead,
                message.Entity.Type.Value,
                message.Entity.OrderMessage?.ResponseId.Value,
                message.Entity.CreationDate,
                message.Entity.OrderMessage?.EnrollmentDate,
                message.Entity.OrderMessage?.IsActive);
        }
        
        protected override Guid GetUserId(ChangeEnrollmentDateCommand command)
        {
            return command.ToUserId;
        }
    }
}