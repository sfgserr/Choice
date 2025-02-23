using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Infrastructure.Data.InternalCommands;
using Users.Infrastructure.MediatR.DomainNotifications;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.EnrollmentDateConfirmed
{
    internal class EnqueueEnrollCommandDomainNotificationHandler : IDomainNotificationHandler<EnrollmentDateConfirmedDomainNotification>
    {
        private readonly CommandsScheduler _scheduler;

        internal EnqueueEnrollCommandDomainNotificationHandler(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Handle(EnrollmentDateConfirmedDomainNotification notification, CancellationToken cancellationToken)
        {
            
        }
    }
}