using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
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
        private readonly IRepository<Message> _repository;

        public OrderCreatedConsumer(IHubContext<ChatHub> context, IRepository<Message> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            OrderCreatedEvent @event = context.Message;

            Message message = new(@event.SenderGuid, @event.ReceiverId, JsonConvert.SerializeObject(@event), MessageType.Order);

            await _repository.Add(message);

            await _context.Clients.User(@event.ReceiverId).SendAsync("OrderCreated", message);
        }
    }
}
