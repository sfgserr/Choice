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
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IHubContext<ChatHub> _context;
        private readonly IMessageRepository _repository;

        public OrderCreatedConsumer(IHubContext<ChatHub> context, IMessageRepository repository)
        {
            _context = context;
            _repository = repository;
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

            await _context.Clients.User(@event.ReceiverId).SendAsync("OrderCreated", message);
        }
    }
}
