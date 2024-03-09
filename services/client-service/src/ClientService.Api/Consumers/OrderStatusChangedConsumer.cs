using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.ClientService.Api.Consumers
{
    public sealed class OrderStatusChangedConsumer : IConsumer<OrderStatusChangedEvent>
    {
        private readonly IClientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderStatusChangedConsumer(IClientRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
        {
            OrderStatusChangedEvent @event = context.Message;
            
            IList<OrderRequest> requests = await _repository.GetRequests();

            OrderRequest request = requests.FirstOrDefault(r => r.Id == @event.OrderRequestId)!;

            request.SetStatus((OrderStatus)@event.OrderStatus);

            _repository.Update(request.Client);

            await _unitOfWork.SaveChanges();
        }
    }
}
