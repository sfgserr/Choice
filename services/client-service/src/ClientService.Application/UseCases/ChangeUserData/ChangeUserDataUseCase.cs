﻿using Choice.ClientService.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.ChangeUserData
{
    public sealed class ChangeUserDataUseCase : IChangeUserDataUseCase
    {
        private readonly IClientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public ChangeUserDataUseCase(IClientRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;

            _outputPort = new ChangeUserDataPresenter();
        }

        public async Task Execute(int clientId, string name, string surname)
        {
            Client client = await _repository.Get(clientId);

            if (client is null)
            {
                _outputPort.NotFound();
                return;
            }

            await ChangeData(client, name, surname);

            _outputPort.Ok(client);
        }

        public async Task ChangeData(Client client, string name, string surname)
        {
            client.ChangeData(name, surname);

            _repository.Update(client);

            await _unitOfWork.SaveChanges();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
