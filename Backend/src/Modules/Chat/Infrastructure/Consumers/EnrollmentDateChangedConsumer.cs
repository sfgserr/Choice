using BuildingBlocks.Application.Events;
using Chat.Application.Messages.Commands.ChangeEnrollmentDate;
using Chat.Infrastructure.Data.Domain.InternalCommands;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Consumers
{
    internal class EnrollmentDateChangedConsumer : IBusConsumer<EnrollmentDateChangedIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        internal EnrollmentDateChangedConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(EnrollmentDateChangedIntegrationEvent @event)
        {
            await _scheduler.EnqueueAsync(new ChangeEnrollmentDateCommand(
                    @event.Id,
                    @event.ResponseId,
                    @event.PreviousEnrollmentDate));
        }
    }
}