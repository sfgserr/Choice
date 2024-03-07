using Choice.ClientService.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.ClientService.Api.Consumers
{
    public class ClientCreatedConsumer : IConsumer<ClientCreatedEvent>
    {
        private readonly IClientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientCreatedConsumer(IClientRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<ClientCreatedEvent> context)
        {
            ClientCreatedEvent @event = context.Message;

            Client client = new
                (@event.UserGuid.ToString(),
                 @event.Name, 
                 @event.Surname, 
                 @event.Email, 
                 new(@event.Street, @event.City),
                 "defaulturi");

            await _repository.Add(client);

            await _unitOfWork.SaveChanges();
        }
    }
}
