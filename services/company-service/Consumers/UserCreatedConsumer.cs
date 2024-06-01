using Choice.Application.Services;
using Choice.Common.ValueObjects;
using Choice.CompanyService.Api.Entities;
using Choice.CompanyService.Api.Repositories;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.CompanyService.Api.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly ICompanyRepository _repository;
        private readonly IAddressService _addressService;

        public UserCreatedConsumer(ICompanyRepository repository, IAddressService addressService)
        {
            _repository = repository;
            _addressService = addressService;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            UserCreatedEvent @event = context.Message;

            if (@event.UserType == "Company")
            {
                Address address = new(@event.Street, @event.City);

                string coords = await _addressService.Geocode(address);

                Company company = new
                    (@event.UserGuid, 
                     @event.Name, 
                     @event.Email, 
                     @event.PhoneNumber, 
                     address,
                     coords);

                await _repository.Add(company);
            }
        }
    }
}
