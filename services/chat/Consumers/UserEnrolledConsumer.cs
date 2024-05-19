using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Models;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.Chat.Api.Services;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Choice.Chat.Api.Consumers
{
    public class UserEnrolledConsumer : IConsumer<UserEnrolledEvent>
    {
        private readonly IMessageRepository _repository;
        private readonly ChatService _chatService;

        public UserEnrolledConsumer(IMessageRepository repository, ChatService chatService)
        {
            _repository = repository;
            _chatService = chatService;
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

            await _repository.Update(message);

            await _chatService.SendMessage(message.SenderId, "enrolled", new(message));
        }
    }
}
