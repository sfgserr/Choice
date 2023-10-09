using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.CreateClient
{
    public class CreateClientValidationUseCase : ICreateClientUseCase
    {
        private readonly ICreateClientUseCase _clientUseCase;
        private readonly IRepository<Client> _clientRepository;

        private IOutputPort _outputPort;

        public CreateClientValidationUseCase(ICreateClientUseCase clientUseCase, IRepository<Client> clientRepository)
        {
            _clientUseCase = clientUseCase;
            _clientRepository = clientRepository;

            _outputPort = new CreateClientPresenter();
        }

        public async Task Execute(string name, string surname, string password, string email, string photoUri)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(photoUri))
            {
                _outputPort.Invalid();
            }

            Client? client = await _clientRepository.GetBy(c => c.Email == email);

            if (client != null)
            {
                _outputPort.Invalid();
            }

            await _clientUseCase.Execute(name, surname, password, email, photoUri);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _clientUseCase.SetOutputPort(outputPort);
        }
    }
}
