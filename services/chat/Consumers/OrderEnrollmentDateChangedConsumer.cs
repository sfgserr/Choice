using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.Models;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Choice.Chat.Api.Consumers
{
    public class OrderEnrollmentDateChangedConsumer : IConsumer<OrderEnrollmentDateChangedEvent>
    {
        private readonly IMessageRepository _repository;
        private readonly IHubContext<ChatHub> _hubContext;

        public OrderEnrollmentDateChangedConsumer(IMessageRepository repository, IHubContext<ChatHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<OrderEnrollmentDateChangedEvent> context)
        {
            OrderEnrollmentDateChangedEvent @event = context.Message;

            Message message = (await _repository.GetByOrderId(@event.OrderId))!;

            message.Content.ChangeContent(o =>
            {
                Order content = (Order)o;

                content.ChangeEnrollmentTime(@event.EnrollmentDate);
            });

            _repository.Update(message);

            Order order = ((Order)message.Content.GetContent()).Copy();

            Message orderMessage = new(message.SenderId, message.ReceiverId, JsonConvert.SerializeObject(order), MessageType.Order);

            await _repository.Add(orderMessage);

            await _hubContext.Clients.User(orderMessage.ReceiverId)
                .SendAsync("EnrollmentChanged", orderMessage, (Order)message.Content.GetContent());
        }
    }
}
