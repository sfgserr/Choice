using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.SendOrderMessage
{
    public class SendOrderMessageUseCase : ISendOrderMessageUseCase
    {
        private readonly IRepository<OrderMessage> _orderMessageRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public SendOrderMessageUseCase(IRepository<OrderMessage> orderMessageRepository, IUnitOfWork unitOfWork)
        {
            _orderMessageRepository = orderMessageRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new SendOrderMessagePresenter();
        }

        public async Task Execute(User sender, Room room, double price, DateTime appointmentTime, int duration, Order order) =>
            await SendOrder(sender, room, price, appointmentTime, duration, order);

        private async Task SendOrder(User sender, Room room, double price, DateTime appointmentTime, int duration, Order order)
        {
            OrderMessage orderMessage = new OrderMessage()
            {
                Sender = sender,
                Room = room,
                Price = price,
                AppointmentTime = appointmentTime,
                Duration = duration,
                Order = order
            };

            OrderMessage createdOrderMessage = await _orderMessageRepository.Create(orderMessage);

            await _unitOfWork.Save();

            _outputPort.Ok(createdOrderMessage);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
