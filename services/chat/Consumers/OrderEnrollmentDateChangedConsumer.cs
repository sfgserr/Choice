using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.Models;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.Chat.Api.Services;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Choice.Chat.Api.Consumers
{
    public class OrderEnrollmentDateChangedConsumer : IConsumer<OrderEnrollmentDateChangedEvent>
    {
        private readonly IMessageRepository _repository;
        private readonly ChatService _chatService;

        public OrderEnrollmentDateChangedConsumer(IMessageRepository repository, ChatService chatService)
        {
            _repository = repository;
            _chatService = chatService;
        }

        public async Task Consume(ConsumeContext<OrderEnrollmentDateChangedEvent> context)
        {
            OrderEnrollmentDateChangedEvent @event = context.Message;

            Message message = (await _repository.GetByOrderId(@event.OrderId))!;

            message.Content.ChangeContent(o =>
            {
                Order content = (Order)o;

                content.ChangeEnrollmentTime(@event.EnrollmentDate, message.ReceiverId != @event.ReceiverId);
            });

            await _repository.Update(message);

            Order order = ((Order)message.Content.GetContent()).Copy();

            Message orderMessage = new(message.SenderId, message.ReceiverId, JsonConvert.SerializeObject(order), MessageType.Order);

            await _repository.Add(orderMessage);

            await _chatService.SendMessage(@event.ReceiverId, "enrollmentDateChanged", new(orderMessage));
        }
    }
}
