using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Application.OrderRequests.Commands.Enroll;
using Users.Infrastructure.Data.InternalCommands;
using Users.Infrastructure.MediatR.DomainNotifications;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers
{
    internal class EnqueueEnrollCommandDomainNotificationHandler :
        IDomainNotificationHandler<EnrolledDomainNotification>
    {
        private readonly CommandsScheduler _scheduler;

        internal EnqueueEnrollCommandDomainNotificationHandler(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Handle(EnrolledDomainNotification notification, CancellationToken cancellationToken)
        {
            await _scheduler.EnqueueAsync(new EnrollCommand(
                Guid.NewGuid(),
                notification.DomainEvent.RequestId.Value));
        }
    }
}