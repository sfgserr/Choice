using Choice.Application.Services;
using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.ChangeUserData
{
    public sealed class ChangeUserDataValidationUseCase : IChangeUserDataUseCase
    {
        private readonly IChangeUserDataUseCase _useCase;
        private readonly IClientRepository _repository;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public ChangeUserDataValidationUseCase(IChangeUserDataUseCase useCase, Notification notification, IClientRepository repository)
        {
            _useCase = useCase;
            _notification = notification;

            _outputPort = new ChangeUserDataPresenter();
            _repository = repository;
        }

        public async Task Execute(string name, string surname, string email, string phoneNumber)
        {
            if (name == string.Empty || surname == string.Empty || email == string.Empty || phoneNumber == string.Empty)
            {
                _notification.Add(nameof(name), "Neither name nor surname should be empty");
            }

            IList<Client> clients = await _repository.GetAll();

            if (clients.Any(c => c.Email == email))
            {
                _notification.Add(nameof(email), "Email already in use");
            }

            if (clients.Any(c => c.PhoneNumber == phoneNumber))
            {
                _notification.Add(nameof(phoneNumber), "Phone number already in use");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(name, surname, email, phoneNumber);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
