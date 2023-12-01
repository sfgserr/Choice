using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.CreateOrder
{
    public class CreateOrderValidationUseCase : ICreateOrderUseCase
    {
        private readonly ICreateOrderUseCase _useCase;

        private IOutputPort _outputPort;

        public CreateOrderValidationUseCase(ICreateOrderUseCase useCase)
        {
            _useCase = useCase;

            _outputPort = new CreateOrderPresenter();
        }

        public async Task Execute(List<Category> categories, string description, bool toKnowPrice, bool toKnowAppointmentTime, bool toKnowDeadLine, List<string> photoUris, int searchingRadius)
        {
            if (categories.Count == 0 || string.IsNullOrEmpty(description) || searchingRadius < 5 || searchingRadius > 25 ||
                (!toKnowAppointmentTime && !toKnowPrice && !toKnowDeadLine))
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(categories, description, toKnowPrice, toKnowAppointmentTime, toKnowDeadLine, photoUris, searchingRadius);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
