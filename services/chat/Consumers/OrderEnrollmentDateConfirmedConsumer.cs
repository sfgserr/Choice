using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Models;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.Chat.Api.Services;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.Chat.Api.Consumers
{
    public class OrderEnrollmentDateConfirmedConsumer : IConsumer<OrderEnrollmentDateConfirmedEvent>
    {
        private readonly IMessageRepository _repository;
        private readonly ChatService _chatService;

        public OrderEnrollmentDateConfirmedConsumer(IMessageRepository repository, ChatService chatService)
        {
            _repository = repository;
            _chatService = chatService;
        }

        public async Task Consume(ConsumeContext<OrderEnrollmentDateConfirmedEvent> context)
        {
            OrderEnrollmentDateConfirmedEvent @event = context.Message;

            Message message = (await _repository.GetByOrderId(@event.OrderId))!;

            message.Content.ChangeContent(o =>
            {
                Order content = (Order)o;

                content.ConfirmDate();
            });

            await _repository.Update(message);

            await _chatService.SendMessage(message.ReceiverId, "confirmed", message);
        }
    }
}
