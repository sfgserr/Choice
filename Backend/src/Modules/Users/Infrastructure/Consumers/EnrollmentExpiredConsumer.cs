using BuildingBlocks.Application.Events;
using Payments.IntegrationEvents;
using Users.Application.OrderResponses.Commands.MarkResponseAsInactive;
using Users.Infrastructure.Data.InternalCommands;

namespace Users.Infrastructure.Consumers
{
    internal class EnrollmentExpiredConsumer : IBusConsumer<EnrollmentExpiredIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        internal EnrollmentExpiredConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(EnrollmentExpiredIntegrationEvent integrationEvent)
        {
            await _scheduler.EnqueueAsync(new MarkResponseAsInactiveCommand(
                integrationEvent.Id,
                integrationEvent.ResponseId));
        }
    }
}