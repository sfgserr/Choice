
namespace Choice.Application.UseCases.Rooms.CreateRoom
{
    public class CreateRoomValidationUseCase : ICreateRoomUseCase
    {
        private readonly ICreateRoomUseCase _useCase;

        private IOutputPort _outputPort;

        public CreateRoomValidationUseCase(ICreateRoomUseCase useCase)
        {
            _useCase = useCase;

            _outputPort = new CreateRoomPresenter();
        }

        public async Task Execute(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(name);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
