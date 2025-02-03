using BuildingBlocks.Application.Events;
using Chat.Application.Chat.Commands.SendOrderMessage;
using Chat.Infrastructure.Processing;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Consumers
{
    internal class EnrollmentDateConfirmedConsumer : IBusConsumer<EnrollmentDateConfirmedIntegrationEvent>
    {
        public async Task Consume(EnrollmentDateConfirmedIntegrationEvent integrationEvent)
        {
            await CommandsExecutor.ExecuteCommandAsync(new SendOrderMessageCommand(
                new
                {
                    integrationEvent.ResponseId,
                    DateConfirmed = true
                }, 
                integrationEvent.ToUserId));
        }
    }
}