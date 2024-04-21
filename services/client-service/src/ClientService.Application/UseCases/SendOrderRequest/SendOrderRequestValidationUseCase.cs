using Choice.Application.Services;

namespace Choice.ClientService.Application.UseCases.SendOrderRequest
{
    public class SendOrderRequestValidationUseCase : ISendOrderRequestUseCase
    {
        private readonly ISendOrderRequestUseCase _useCase;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public SendOrderRequestValidationUseCase(ISendOrderRequestUseCase useCase, Notification notification)
        {
            _useCase = useCase;
            _notification = notification;

            _outputPort = new SendOrderRequestPresenter();
        }

        public async Task Execute(string description, List<string> photoUris, int categoryId, int searchRadius,
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
                (description,
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
