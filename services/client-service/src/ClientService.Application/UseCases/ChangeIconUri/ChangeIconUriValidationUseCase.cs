using Choice.Application.Services;

namespace Choice.ClientService.Application.UseCases.ChangeIconUri
{
    public sealed class ChangeIconUriValidationUseCase : IChangeIconUriUseCase
    {
        private readonly IChangeIconUriUseCase _useCase;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public ChangeIconUriValidationUseCase(IChangeIconUriUseCase useCase, Notification notification)
        {
            _useCase = useCase;
            _notification = notification;

            _outputPort = new ChangeIconUriPresenter();
        }

        public async Task Execute(string iconUri)
        {
            if (iconUri == string.Empty)
            {
                _notification.Add(nameof(iconUri), "Icon uri can't be empty");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(iconUri);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
