using Chat.Api.Services;
using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.Models;
using Choice.Chat.Api.Repositories;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.Chat.Api.Services;
using Choice.EventBus.Messages.Events;
using FirebaseAdmin.Messaging;
using MassTransit;
using Newtonsoft.Json;

namespace Choice.Chat.Api.Consumers
{
    public class OrderEnrollmentDateChangedConsumer : IConsumer<OrderEnrollmentDateChangedEvent>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly ChatService _chatService;

        public OrderEnrollmentDateChangedConsumer(IMessageRepository messageRepository, IUserRepository userRepository, 
            ChatService chatService)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _chatService = chatService;
        }

        public async Task Consume(ConsumeContext<OrderEnrollmentDateChangedEvent> context)
        {
            OrderEnrollmentDateChangedEvent @event = context.Message;

            Entities.Message message = (await _messageRepository.GetByOrderId(@event.OrderId))!;

            string senderId = message.ReceiverId != @event.ReceiverId ? message.ReceiverId : message.SenderId;

            message.Content.ChangeContent(o =>
            {
                Order content = (Order)o;

                content.ChangeEnrollmentTime(@event.EnrollmentDate, @event.IsClientChanged, senderId);

                return JsonConvert.SerializeObject(content);
            });

            await _messageRepository.Update(message);

            Order order = ((Order)message.Content.GetContent()).Copy();

            Entities.Message orderMessage = new
                (senderId, @event.ReceiverId, JsonConvert.SerializeObject(order), MessageType.Order);

            await _messageRepository.Add(orderMessage);

            await _chatService.SendMessage(@event.ReceiverId, "enrollmentDateChanged", new(orderMessage));

            User user = (await _userRepository.Get(@event.ReceiverId))!;

            await NotificationService.SendNotificationAsync(
                "Дата записи поменялась",
                "Дата записи поменялась", 
                user.DeviceTokens);
        }
    }
}
