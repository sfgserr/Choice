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
        private readonly IRepository<Message> _repository;
        private readonly IHubContext<ChatHub> _hubContext;

        public OrderEnrollmentDateChangedConsumer(IRepository<Message> repository, IHubContext<ChatHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<OrderEnrollmentDateChangedEvent> context)
        {
            OrderEnrollmentDateChangedEvent @event = context.Message;

            
        }
    }
}
