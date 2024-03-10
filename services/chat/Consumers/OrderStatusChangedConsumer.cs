using Choice.Chat.Api.Hubs;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Consumers
{
    public class OrderStatusChangedConsumer : IConsumer<OrderStatusChangedEvent>
    {
        private readonly IHubContext<ChatHub> _context;

        public OrderStatusChangedConsumer(IHubContext<ChatHub> context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
        {
            OrderStatusChangedEvent @event = context.Message;

            await _context.Clients.Users(@event.OrderReceiverId).SendAsync("OrderChanged", @event);
        }
    }
}
