using Choice.Chat.Api.Hubs;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IHubContext<ChatHub> _context;

        public OrderCreatedConsumer(IHubContext<ChatHub> context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            OrderCreatedEvent @event = context.Message;

            await _context.Clients.User(@event.ReceiverId).SendAsync("OrderCreated", @event);
        }
    }
}
