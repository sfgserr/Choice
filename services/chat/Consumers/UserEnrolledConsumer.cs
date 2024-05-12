using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.Models;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Consumers
{
    public class UserEnrolledConsumer : IConsumer<UserEnrolledEvent>
    {
        private readonly IMessageRepository _repository;
        private readonly IHubContext<ChatHub> _hubContext;

        public UserEnrolledConsumer(IMessageRepository repository, IHubContext<ChatHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<UserEnrolledEvent> context)
        {
            UserEnrolledEvent @event = context.Message;

            Message message = (await _repository.GetByOrderId(@event.OrderId))!;

            message.Content.ChangeContent(o =>
            {
                Order order = (Order)o;

                order.Enroll();
            });

            _repository.Update(message);

            await _hubContext.Clients.User(message.SenderId).SendAsync("Enrolled", new { message });
        }
    }
}
