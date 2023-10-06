using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.SendOrderMessage
{
    public class SendOrderMessageValidationUseCase : ISendOrderMessageUseCase
    {
        private readonly ISendOrderMessageUseCase _useCase;

        private IOutputPort _outputPort;

        public SendOrderMessageValidationUseCase(ISendOrderMessageUseCase useCase)
        {
            _useCase = useCase;

            _outputPort = new SendOrderMessagePresenter();
        }

        public async Task Execute(User sender, User receiver, double price, DateTime appointmentTime, int duration, Order order)
        {
            if (order is null || sender is null || receiver is null || price <= 0 || duration < 0)
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(sender, receiver, price, appointmentTime, duration, order); 
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
