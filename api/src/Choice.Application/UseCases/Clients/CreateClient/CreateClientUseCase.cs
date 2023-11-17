﻿using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.CreateClient
{
    public class CreateClientUseCase : ICreateClientUseCase
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public CreateClientUseCase(IRepository<Client> clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new CreateClientPresenter();
        }

        public async Task Execute(string name, string surname, string password, string email, string photoUri) =>
            await CreateClient(name, surname, password, email, photoUri);

        private async Task CreateClient(string name, string surname, string password, string email, string iconUri)
        {
            Client client = new Client()
            {
                Name = name,
                Surname = surname,
                Password = password,
                Email = email,
                IconUri = iconUri
            };

            Client newClient = await _clientRepository.Create(client);

            await _unitOfWork.Save();

            _outputPort.Ok(newClient);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
