using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.UpdateOrderMessage
{
    public class UpdateOrderMessageUseCase : IUpdateOrderMessageUseCase
    {
        private readonly IRepository<OrderMessage> _orderMessageRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public UpdateOrderMessageUseCase(IRepository<OrderMessage> orderMessageRepository, IUnitOfWork unitOfWork)
        {
            _orderMessageRepository = orderMessageRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new UpdateOrderMessagePresenter();
        }

        public async Task Execute(OrderMessage orderMessage)
        {
            OrderMessage updatedOrderMessage = await _orderMessageRepository.Update(orderMessage);

            await _unitOfWork.Save();

            _outputPort.Ok(updatedOrderMessage);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
