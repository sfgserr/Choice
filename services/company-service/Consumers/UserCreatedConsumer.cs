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

        public UserCreatedConsumer(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            UserCreatedEvent @event = context.Message;

            if (@event.UserType == "Company")
            {
                Company company = new
                    (@event.UserGuid, 
                     @event.Name, 
                     @event.Email, 
                     @event.PhoneNumber, 
                     new(@event.Street, @event.City));

                await _repository.Add(company);
            }
        }
    }
}
