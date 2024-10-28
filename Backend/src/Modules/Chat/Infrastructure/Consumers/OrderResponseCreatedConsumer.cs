using BuildingBlocks.Application.Events;
using Chat.Application.Messages.Commands.CreateOrderMessage;
using Chat.Infrastructure.Data.Domain.InternalCommands;
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

        public async Task Consume(OrderResponseCreatedIntegrationEvent integrationEvent)
        {
            await _scheduler.EnqueueAsync(new CreateOrderMessageCommand(
                integrationEvent.Id,
                integrationEvent.ResponseId,
                integrationEvent.FromUserId,
                integrationEvent.ToUserId));
        }
    }
}