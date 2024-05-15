using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Models;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.Chat.Api.Services;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Newtonsoft.Json;

namespace Choice.Chat.Api.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IMessageRepository _repository;
        private readonly ChatService _chatService;

        public OrderCreatedConsumer(IMessageRepository repository, ChatService chatService)
        {
            _repository = repository;
            _chatService = chatService;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            OrderCreatedEvent @event = context.Message;

            Order order = new(
                @event.OrderId, 
                @event.OrderRequestId, 
                @event.Price, 
                @event.Prepayment, 
                @event.Deadline, 
                @event.IsEnrolled, 
                @event.EnrollmentTime, 
                @event.Status);

            Message message = new(@event.SenderGuid, @event.ReceiverId, JsonConvert.SerializeObject(order), MessageType.Order);

            await _repository.Add(message);

            await _chatService.SendMessage(message.ReceiverId, "orderCreated", message);
        }
    }
}
