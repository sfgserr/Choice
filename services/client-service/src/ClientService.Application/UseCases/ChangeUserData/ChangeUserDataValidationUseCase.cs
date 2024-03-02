using Choice.ClientService.Application.Services;

namespace Choice.ClientService.Application.UseCases.ChangeUserData
{
    public sealed class ChangeUserDataValidationUseCase : IChangeUserDataUseCase
    {
        private readonly IChangeUserDataUseCase _useCase;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public ChangeUserDataValidationUseCase(IChangeUserDataUseCase useCase, Notification notification)
        {
            _useCase = useCase;
            _notification = notification;

            _outputPort = new ChangeUserDataPresenter();
        }

        public async Task Execute(int clientId, string name, string surname)
        {
            if (name == string.Empty || surname == string.Empty)
            {
                _notification.Add(nameof(name), "Neither name nor surname should be empty");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(clientId, name, surname);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
