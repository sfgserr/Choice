using Choice.Chat.Api.Hubs;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Consumers
{
    public class OrderChangedConsumer : IConsumer<OrderChangedEvent>
    {
        private readonly IHubContext<ChatHub> _context;

        public OrderChangedConsumer(IHubContext<ChatHub> context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<OrderChangedEvent> context)
        {
            OrderChangedEvent @event = context.Message;

            await _context.Clients.Users(@event.ReceiverId).SendAsync("OrderChanged", @event);
        }
    }
}
