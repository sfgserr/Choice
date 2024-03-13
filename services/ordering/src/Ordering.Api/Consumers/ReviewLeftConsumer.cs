using Choice.EventBus.Messages.Events;
using Choice.Ordering.Domain.OrderEntity;
using MassTransit;

namespace Choice.Ordering.Api.Consumers
{
    public class ReviewLeftConsumer : IConsumer<ReviewLeftEvent>
    {
        private readonly IOrderRepository _repository;

        public ReviewLeftConsumer(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ReviewLeftEvent> context)
        {
            ReviewLeftEvent @event = context.Message;

            Order order = await _repository.GetOrder(@event.OrderId);

            order.AddReview(@event.UserGuid);

            _repository.Update(order);
        }
    }
}
