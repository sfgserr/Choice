using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Factories;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Consumers
{
    public class OrderEnrollmentDateChangedConsumer : IConsumer<OrderEnrollmentDateChangedEvent>
    {
        private readonly IRepository<Order> _repository;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly OrderFactory _factory;

        public OrderEnrollmentDateChangedConsumer(IRepository<Order> repository, IHubContext<ChatHub> hubContext, OrderFactory factory)
        {
            _repository = repository;
            _hubContext = hubContext;
            _factory = factory;
        }

        public async Task Consume(ConsumeContext<OrderEnrollmentDateChangedEvent> context)
        {
            OrderEnrollmentDateChangedEvent @event = context.Message;

            Order order = await _repository.Get(@event.OrderId);

            order.ChangeEnrollmentDate(@event.EnrollmentDate);

            await _repository.Update(order);

            Order newOrder = _factory.Copy(order);
            newOrder.ChangeEnrollmentDate(@event.EnrollmentDate);

            await _repository.Add(order);

            await _hubContext.Clients.Users(order.ReceiverId).SendAsync("OrderEnrollmentDateChanged", order);
        }
    }
}
