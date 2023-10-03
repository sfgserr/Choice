using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Clients.CreateClient
{
    public class CreateClientUseCaseValidation : ICreateClientUseCase
    {
        private readonly ICreateClientUseCase _clientUseCase;
        private readonly IRepository<User> _userRepository;

        private IOutputPort _outputPort;

        public CreateClientUseCaseValidation(ICreateClientUseCase clientUseCase, IRepository<User> userRepository)
        {
            _clientUseCase = clientUseCase;
            _userRepository = userRepository;

            _outputPort = new CreateClientUseCasePresenter();
        }

        public async Task Execute(string name, string surname, string password, string email, string photoUri)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(photoUri))
            {
                _outputPort.Invalid();
            }

            User? user = await _userRepository.GetUserBy(c => c.Email == email);

            if (user != null)
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
