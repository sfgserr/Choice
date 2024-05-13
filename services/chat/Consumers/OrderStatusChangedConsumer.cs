using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.Models;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Consumers
{
    public class OrderStatusChangedConsumer : IConsumer<OrderStatusChangedEvent>
    {
        private readonly IHubContext<ChatHub> _context;
        private readonly IMessageRepository _repository;

        public OrderStatusChangedConsumer(IHubContext<ChatHub> context, IMessageRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
        {
            OrderStatusChangedEvent @event = context.Message;

            Message message = (await _repository.GetByOrderRequestId(@event.OrderRequestId))!;

            message.Content.ChangeContent(o =>
            {
                Order order = (Order)o;

                order.ChangeStatus(@event.OrderStatus);
            });

            _repository.Update(message);

            await _context.Clients.User(@event.ReceiverId).SendAsync("StatusChanged", new { message });
        }
    }
}
