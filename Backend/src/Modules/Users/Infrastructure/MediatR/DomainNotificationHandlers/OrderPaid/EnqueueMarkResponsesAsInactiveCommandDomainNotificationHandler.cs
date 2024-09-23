using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Application.OrderResponses.Commands.MarkResponsesAsInactive;
using Users.Infrastructure.Data.InternalCommands;
using Users.Infrastructure.MediatR.DomainNotifications;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.OrderPaid
{
    internal class EnqueueMarkResponsesAsInactiveCommandDomainNotificationHandler :
        IDomainNotificationHandler<OrderPaidDomainNotification>
    {
        private readonly CommandsScheduler _scheduler;

        internal EnqueueMarkResponsesAsInactiveCommandDomainNotificationHandler(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Handle(OrderPaidDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            
            await _scheduler.EnqueueAsync(new MarkResponsesAsInactiveCommand(
                Guid.NewGuid(),
                domainEvent.ResponseId.Value,
                domainEvent.RequestId.Value));
        }
    }
}