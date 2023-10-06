using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.UpdateClient
{
    public class UpdateClientUseCase : IUpdateClientUseCase
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public UpdateClientUseCase(IRepository<Client> clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new UpdateClientPresenter();
        }

        public async Task Execute(Client client) => 
            await UpdateClient(client);

        private async Task UpdateClient(Client client)
        {
            Client updatedClient = await _clientRepository.Update(client);

            await _unitOfWork.Save();

            _outputPort.Ok(updatedClient);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
