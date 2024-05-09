using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IHubContext<ChatHub> _context;
        private readonly IRepository<Order> _repository;

        public OrderCreatedConsumer(IHubContext<ChatHub> context, IRepository<Order> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            OrderCreatedEvent @event = context.Message;

            Order order = new(@event.OrderId,
                              @event.Price,
                              @event.Deadline,
                              @event.EnrollmentTime,
                              @event.Prepayment,
                              @event.SenderGuid,
                              @event.ReceiverId);

            await _repository.Add(order);

            await _context.Clients.User(@event.ReceiverId).SendAsync("OrderCreated", order);
        }
    }
}
