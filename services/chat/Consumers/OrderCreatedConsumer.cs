using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Models;
using Choice.Chat.Api.Repositories;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.Chat.Api.Services;
using Choice.Chat.Api.ViewModels;
using Choice.EventBus.Messages.Events;
using FirebaseAdmin.Messaging;
using MassTransit;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Choice.Chat.Api.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly ChatService _chatService;

        public OrderCreatedConsumer(IMessageRepository repository, IUserRepository userRepository, ChatService chatService)
        {
            _messageRepository = repository;
            _userRepository = userRepository;
            _chatService = chatService;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            OrderCreatedEvent @event = context.Message;

            Order order = new(
                @event.OrderId, 
                @event.OrderRequestId, 
                @event.Price, 
                @event.Prepayment, 
                @event.Deadline, 
                @event.IsEnrolled, 
                @event.EnrollmentTime, 
                @event.Status);

            Entities.Message message = new(@event.SenderGuid, @event.ReceiverId, JsonConvert.SerializeObject(order), MessageType.Order);

            await _messageRepository.Add(message);

            await _chatService.SendMessage(message.ReceiverId, "orderCreated", new(message));

            User user = (await _userRepository.Get(@event.ReceiverId))!;

            foreach (string deviceToken in user.DeviceTokens)
            {
                FirebaseAdmin.Messaging.Message notification = new()
                {
                    Data = new Dictionary<string, string>()
                    {
                        { "Сообщение", "Компания ответила на ваш заказ" }
                    },
                    Notification = new Notification()
                    {
                        Title = "Новое сообщение",
                        Body = "У вас новое сообщение"
                    },
                    Token = deviceToken
                };

                await FirebaseMessaging.DefaultInstance.SendAsync(notification);
            }
        }
    }
}
