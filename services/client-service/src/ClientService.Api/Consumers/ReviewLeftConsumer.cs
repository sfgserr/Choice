using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.ClientService.Api.Consumers
{
    public class ReviewLeftConsumer : IConsumer<ReviewLeftEvent>
    {
        private readonly IClientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewLeftConsumer(IClientRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<ReviewLeftEvent> context)
        {
            ReviewLeftEvent @event = context.Message;

            Client client = await _repository.Get(@event.UserGuid);

            if (client is null)
                return;

            client.AddReview(@event.Grade);

            await _unitOfWork.SaveChanges();
        }
    }
}
