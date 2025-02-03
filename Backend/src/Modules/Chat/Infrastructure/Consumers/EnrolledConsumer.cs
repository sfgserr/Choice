using BuildingBlocks.Application.Events;
using Chat.Application.Chat.Commands.SendOrderMessage;
using Chat.Infrastructure.Processing;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Consumers
{
    internal class EnrolledConsumer : IBusConsumer<EnrolledIntegrationEvent>
    {
        public async Task Consume(EnrolledIntegrationEvent integrationEvent)
        {
            await CommandsExecutor.ExecuteCommandAsync(new SendOrderMessageCommand(
                new
                {
                    integrationEvent.ResponseId,
                    IsEnrolled = true
                },
                integrationEvent.CompanyId));
        }
    }
}