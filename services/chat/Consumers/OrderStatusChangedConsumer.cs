using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Consumers
{
    public class OrderStatusChangedConsumer : IConsumer<OrderStatusChangedEvent>
    {
        private readonly IHubContext<ChatHub> _context;
        private readonly IRepository<Order> _repository;

        public OrderStatusChangedConsumer(IHubContext<ChatHub> context, IRepository<Order> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
        {
            OrderStatusChangedEvent @event = context.Message;

            Order order = await _repository.Get(@event.OrderRequestId);

            order.ChangeStatus(@event.OrderStatus);

            await _repository.Update(order);

            await _context.Clients.User(order.ReceiverId).SendAsync("StatusChanged", new { order.Status });
        }
    }
}
