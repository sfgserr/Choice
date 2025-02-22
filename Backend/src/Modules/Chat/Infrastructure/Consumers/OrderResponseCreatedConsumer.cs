using BuildingBlocks.Application.Events;
using Chat.Application.Messages.Commands.CreateOrderMessage;
using Chat.Infrastructure.Data.InternalCommands;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Consumers
{
    internal class OrderResponseCreatedConsumer : IBusConsumer<OrderResponseCreatedIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        internal OrderResponseCreatedConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(OrderResponseCreatedIntegrationEvent @event)
        {
            await _scheduler.EnqueueAsync(new CreateOrderMessageCommand(
                @event.Id,
                @event.ResponseId,
                @event.FromUserId,
                @event.ToUserId));
        }
    }
}