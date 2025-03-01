using BuildingBlocks.Application.Events;
using Chat.Application.Chat.Commands.SendOrderMessage;
using Chat.Infrastructure.Processing;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Consumers
{
    internal class EnrollmentDateConfirmedConsumer : IBusConsumer<EnrollmentDateConfirmedIntegrationEvent>
    {
        public async Task Consume(EnrollmentDateConfirmedIntegrationEvent @event)
        {
            await CommandsExecutor.ExecuteCommandAsync(new SendOrderMessageCommand(
                new
                {
                    @event.ResponseId,
                    IsEnrollmentDateConfirmed = true,
                    IsEnrolled = true
                }, 
                @event.ToUserId));
        }
    }
}