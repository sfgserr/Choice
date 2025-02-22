using BuildingBlocks.Application.Events;
using Chat.Application.Chat.Commands.SendOrderMessage;
using Chat.Infrastructure.Processing;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Consumers
{
    public class OrderStatusChangedConsumer : IBusConsumer<OrderStatusChangedIntegrationEvent>
    {
        public async Task Consume(OrderStatusChangedIntegrationEvent @event)
        {
            await CommandsExecutor.ExecuteCommandAsync(new SendOrderMessageCommand(
                new
                {
                    @event.ResponseId,
                    @event.Status
                },
                @event.ToUserId));
        }
    }
}