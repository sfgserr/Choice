using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using BuildingBlocks.Infrastructure.InternalCommands;
using Users.Application.Users.Commands.Review;
using Users.Infrastructure.MediatR.DomainNotifications;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers
{
    internal class ReviewCreatedDomainNotificationHandler : IDomainNotificationHandler<ReviewCreatedDomainNotification>
    {
        private readonly ICommandsScheduler _scheduler;

        internal ReviewCreatedDomainNotificationHandler(ICommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Handle(ReviewCreatedDomainNotification notification, CancellationToken cancellationToken)
        {
            await _scheduler.EnqueueAsync(new ReviewCommand(
                Guid.NewGuid(),
                notification.DomainEvent.Grade,
                notification.DomainEvent.ToUserId.Value));
        }
    }
}