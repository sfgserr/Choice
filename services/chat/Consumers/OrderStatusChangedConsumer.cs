﻿using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Hubs;
using Choice.Chat.Api.Models;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.Chat.Api.Services;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Choice.Chat.Api.Consumers
{
    public class OrderStatusChangedConsumer : IConsumer<OrderStatusChangedEvent>
    {
        private readonly IMessageRepository _repository;
        private readonly ChatService _chatService;

        public OrderStatusChangedConsumer(IMessageRepository repository, ChatService chatService)
        {
            _repository = repository;
            _chatService = chatService;
        }

        public async Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
        {
            OrderStatusChangedEvent @event = context.Message;

            Message message = (await _repository.GetByOrderId(@event.OrderId))!;

            message.Content.ChangeContent(o =>
            {
                Order order = (Order)o;

                order.ChangeStatus(@event.OrderStatus);

                return JsonConvert.SerializeObject(order);
            });

            await _repository.Update(message);

            await _chatService.SendMessage(@event.ReceiverId, "statusChanged", new(message));
        }
    }
}
