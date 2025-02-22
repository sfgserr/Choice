using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Application.OrderRequests.Commands.SetStatus;
using Users.Infrastructure.Data.InternalCommands;
using Users.Infrastructure.MediatR.DomainNotifications;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.OrderStatusChanged
{
    internal class OrderStatusChangedDomainNotificationHandler :
        IDomainNotificationHandler<OrderStatusChangedDomainNotification>
    {
        private readonly CommandsScheduler _scheduler;

        internal OrderStatusChangedDomainNotificationHandler(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Handle(OrderStatusChangedDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            await _scheduler.EnqueueAsync(new SetStatusCommand(
                notification.Id,
                domainEvent.RequestId.Value,
                domainEvent.Status));
        }
    }
}
