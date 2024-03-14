﻿using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.ClientService.Api.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly IClientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserCreatedConsumer(IClientRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            UserCreatedEvent @event = context.Message;

            if (@event.UserType == "Company")
                return;

            string[] name = @event.Name.Split(' ');

            Client client = new
                (@event.UserGuid.ToString(),
                 name[0],
                 name[1],
                 @event.Email,
                 new(@event.Street, @event.City),
                 "defaulturi",
                 @event.PhoneNumber);

            await _repository.Add(client);

            await _unitOfWork.SaveChanges();
        }
    }
}