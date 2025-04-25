using BuildingBlocks.Application.Events;
using Payments.Application.Wallets.Commands.CreateWallet;
using Payments.Infrastructure.Data.InternalCommands;
using Users.IntegrationEvents;

namespace Payments.Infrastructure.Consumers
{
    internal class UserCreatedConsumer : IBusConsumer<UserCreatedIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        internal UserCreatedConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(UserCreatedIntegrationEvent @event)
        {
            await _scheduler.EnqueueAsync(new CreateWalletCommand(@event.Id, @event.UserId));
        }
    }
}