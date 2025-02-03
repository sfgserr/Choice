using BuildingBlocks.Application.Events;
using Chat.Application.Chat.Commands.SendOrderMessage;
using Chat.Infrastructure.Processing;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Consumers
{
    public class OrderStatusChangedConsumer : IBusConsumer<OrderStatusChangedIntegrationEvent>
    {
        public async Task Consume(OrderStatusChangedIntegrationEvent integrationEvent)
        {
            await CommandsExecutor.ExecuteCommandAsync(new SendOrderMessageCommand(
                new
                {
                    integrationEvent.ResponseId,
                    integrationEvent.Status
                },
                integrationEvent.ToUserId));
        }
    }
}