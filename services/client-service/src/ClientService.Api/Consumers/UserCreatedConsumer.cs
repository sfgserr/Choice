using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.Common.ValueObjects;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.ClientService.Api.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly IClientRepository _repository;
        private readonly IAddressService _addressService;
        private readonly IUnitOfWork _unitOfWork;

        public UserCreatedConsumer(IClientRepository repository, IUnitOfWork unitOfWork, IAddressService addressService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _addressService = addressService;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            UserCreatedEvent @event = context.Message;

            if (@event.UserType == "Client")
            {
                string[] name = @event.Name.Split('_');

                Address address = new(@event.Street, @event.City);

                string coords = await _addressService.Geocode(address);

                Client client = new
                    (@event.UserGuid.ToString(),
                     name[0],
                     name[1],
                     @event.Email,
                     address,
                     coords,
                     "defaulturi",
                     @event.PhoneNumber);

                await _repository.Add(client);

                await _unitOfWork.SaveChanges();
            }
        }
    }
}
