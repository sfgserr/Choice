using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using BuildingBlocks.Infrastructure.InternalCommands;
using Users.Application.OrderRequests.Commands.SetStatus;
using Users.Infrastructure.MediatR.DomainNotifications;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers
{
    internal class OrderStatusChangedDomainNotificationHandler :
        IDomainNotificationHandler<OrderStatusChangedDomainNotification>
    {
        private readonly ICommandsScheduler _scheduler;

        internal OrderStatusChangedDomainNotificationHandler(ICommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Handle(OrderStatusChangedDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            await _scheduler.EnqueueAsync(new SetStatusCommand(
                notification.Id,
                domainEvent.RequestId.Value,
                domainEvent.Status.Value));
        }
    }
}
