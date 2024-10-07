using BuildingBlocks.Application.Events;
using Payments.IntegrationEvents;
using Users.Application.OrderResponses.Commands.MarkAsPaid;
using Users.Infrastructure.Data.InternalCommands;

namespace Users.Infrastructure.Consumers
{
    internal class EnrollmentPaidConsumer : IBusConsumer<EnrollmentPaidIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        internal EnrollmentPaidConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(EnrollmentPaidIntegrationEvent integrationEvent)
        {
            await _scheduler.EnqueueAsync(new MarkAsPaidCommand(
                integrationEvent.Id,
                integrationEvent.ResponseId));
        }
    }
}
