using BuildingBlocks.Application.Extensions;
using Chat.Application.Contracts;
using Chat.Application.RealTimeMessaging;
using Chat.Domain.Messages;
using Chat.Domain.Messages.OrderMessages;

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
            var orderMessage = _dbContext.Messages
                .TranslatedWhere<Message, MessageDataModel>(m => m.Type.Equals(MessageType.Order) && m.OrderMessage!.ResponseId.Equals(new OrderResponseId(
                    command.ResponseId)))
                .TranslatedOrderByDescending<Message, MessageDataModel, DateTime>(m => m.CreationDate)
                .FirstOrDefault();
            
            var newOrderMessage = orderMessage!.ChangeEnrollmentDate(command.EnrollmentDate.ToUniversalTime());

            var message = await _dbContext.Messages.AddAsync(newOrderMessage);

            return message.Entity.ToDto<MessageDto>();
        }
        
        protected override Guid GetUserId(ChangeEnrollmentDateCommand command)
        {
            return command.ToUserId;
        }
    }
}