using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Handlers;
using Payments.Application.Subscriptions.Commands.Subscribe;
using Payments.Infrastructure.Data.InternalCommands;
using Payments.Infrastructure.MediatR.DomainNotifications;

namespace Payments.Infrastructure.MediatR.DomainNotificationHandlers.SubscriptionPaymentPaidDomainNotificationHandler
{
    internal class SubscriptionPaymentDomainNotificationHandler : 
        IDomainNotificationHandler<SubscriptionPaymentPaidDomainNotification>
    {
        private readonly CommandsScheduler _commandsScheduler;

        internal SubscriptionPaymentDomainNotificationHandler(CommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(SubscriptionPaymentPaidDomainNotification notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            
            await _commandsScheduler.EnqueueAsync(new SubscribeCommand(
                domainEvent.Id,
                domainEvent.PayerId.Value,
                domainEvent.PeriodName));
        }
    }
}