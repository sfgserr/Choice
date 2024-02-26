using Ordering.Application.Services;

namespace Choice.Ordering.Application.UseCases.CreateOrder
{
    public sealed class CreateOrderValidationUseCase : ICreateOrderUseCase
    {
        private readonly ICreateOrderUseCase _useCase;
        private readonly Notification _notification;

        private IOutputPort _outputPort;

        public CreateOrderValidationUseCase(ICreateOrderUseCase useCase, Notification notification)
        {
            _useCase = useCase;
            _notification = notification;

            _outputPort = new CreateOrderPresenter();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(_outputPort);
        }

        public async Task Execute(string receiverId, int orderRequestId, double price, double prepayment,
            int deadline, DateTime enrollmentDate)
        {
            if (price == 0 && deadline == 0)
            {
                _notification.Add($"{nameof(price)},{nameof(deadline)}", "At least one property should have value");
            }

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(receiverId, orderRequestId, price, prepayment, deadline, enrollmentDate);
        }
    }
}
