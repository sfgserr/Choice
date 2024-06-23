using Choice.Application.Services;

namespace Choice.ClientService.Application.UseCases.ChangeIconUriAdmin
{
    public sealed class ChangeIconUriAdminValidationUseCase : IChangeIconUriAdminUseCase
    {
        private readonly IChangeIconUriAdminUseCase _useCase;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public ChangeIconUriAdminValidationUseCase(IChangeIconUriAdminUseCase useCase, Notification notification)
        {
            _useCase = useCase;
            _notification = notification;

            _outputPort = new ChangeIconUriAdminPresenter();
        }

        public async Task Execute(string id, string iconUri)
        {
            if (id == string.Empty)
            {
                _notification.Add(nameof(iconUri), "Id can't be empty");
            }

            if (iconUri == string.Empty)
            {
                _notification.Add(nameof(iconUri), "Icon uri can't be empty");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(id, iconUri);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
