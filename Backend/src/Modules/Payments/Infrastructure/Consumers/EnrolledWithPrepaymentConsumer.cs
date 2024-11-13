using BuildingBlocks.Application.Events;
using Payments.Application.EnrollmentPayments.Commands.Buy;
using Payments.Infrastructure.Data.InternalCommands;
using Users.IntegrationEvents;

namespace Payments.Infrastructure.Consumers
{
    internal class EnrolledWithPrepaymentConsumer : IBusConsumer<EnrolledWithPrepaymentIntegrationEvent>
    {
        private readonly CommandsScheduler _commandsScheduler;

        internal EnrolledWithPrepaymentConsumer(CommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Consume(EnrolledWithPrepaymentIntegrationEvent integrationEvent)
        {
            await _commandsScheduler.EnqueueAsync(new BuyCommand(
                integrationEvent.Id,
                integrationEvent.ResponseId,
                integrationEvent.ClientId,
                integrationEvent.Cost,
                integrationEvent.Currency));
        }
    }
}
