using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Consumers
{
    public class UserEnrolledConsumer : IConsumer<UserEnrolledEvent>
    {
        private readonly IRepository<Order> _repository;
        private readonly IHubContext<ChatHub> _hubContext;

        public UserEnrolledConsumer(IRepository<Order> repository, IHubContext<ChatHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<UserEnrolledEvent> context)
        {
            UserEnrolledEvent @event = context.Message;

            Order order = await _repository.Get(@event.OrderId);

            order.Enroll();

            await _repository.Update(order);

            await _hubContext.Clients.User(order.ReceiverId).SendAsync("Enrolled", new { order.OrderId });
        }
    }
}
