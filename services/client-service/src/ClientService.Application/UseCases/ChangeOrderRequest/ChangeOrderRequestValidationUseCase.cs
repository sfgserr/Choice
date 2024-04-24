using Choice.Application.Services;

namespace Choice.ClientService.Application.UseCases.ChangeOrderRequest
{
    public sealed class ChangeOrderRequestValidationUseCase : IChangeOrderRequestUseCase
    {
        private readonly IChangeOrderRequestUseCase _useCase;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public ChangeOrderRequestValidationUseCase(IChangeOrderRequestUseCase useCase, Notification notification)
        {
            _useCase = useCase;
            _notification = notification;

            _outputPort = new ChangeOrderRequestPresenter();
        }

        public async Task Execute(int requestId, string description, List<string> photoUris, int categoryId, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            if (description == string.Empty)
            {
                _notification.Add(nameof(description), "Description is empty");
            }

            if (!toKnowPrice && !toKnowDeadline && !toKnowEnrollmentDate)
            {
                _notification.Add(nameof(toKnowPrice), "At least one parameter should be true");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute
                (requestId,
                 description,
                 photoUris,
                 categoryId,
                 searchRadius,
                 toKnowPrice,
                 toKnowDeadline,
                 toKnowEnrollmentDate);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
