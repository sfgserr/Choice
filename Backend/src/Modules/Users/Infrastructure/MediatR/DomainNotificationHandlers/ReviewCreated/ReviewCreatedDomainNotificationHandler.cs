using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Users.Application.Users.Commands.Review;
using Users.Infrastructure.Data.InternalCommands;
using Users.Infrastructure.MediatR.DomainNotifications;

namespace Users.Infrastructure.MediatR.DomainNotificationHandlers.ReviewCreated
{
    internal class ReviewCreatedDomainNotificationHandler : IDomainNotificationHandler<ReviewCreatedDomainNotification>
    {
        private readonly CommandsScheduler _scheduler;

        internal ReviewCreatedDomainNotificationHandler(CommandsScheduler scheduler)
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