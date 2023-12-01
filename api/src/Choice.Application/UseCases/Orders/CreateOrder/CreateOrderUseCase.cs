using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.CreateOrder
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public CreateOrderUseCase(IRepository<Order> orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new CreateOrderPresenter();
        }

        public async Task Execute(List<Category> categories, string description, bool toKnowPrice, bool toKnowAppointmentTime, bool toKnowDeadLine, List<string> photoUris, int searchingRadius) =>
            await CreateOrder(categories, description, toKnowPrice, toKnowAppointmentTime, toKnowDeadLine, photoUris, searchingRadius);

        private async Task CreateOrder(List<Category> categories, string description, bool toKnowPrice, bool toKnowAppointmentTime, bool toKnowDeadLine, List<string> photoUris, int searchingRadius)
        {
            Order order = new Order()
            {
                Categories = categories,
                Description = description,
                ToKnowPrice = toKnowPrice,
                ToKnowDeadLine = toKnowDeadLine,
                ToKnowAppointmentTime = toKnowAppointmentTime,
                IsClientAppointed = false,
                TimeCreated = DateTime.Now,
                Status = OrderStatus.Active,
                PhotoUris = photoUris,
                SearchingRadius = searchingRadius
            };

            Order createdOrder = await _orderRepository.Create(order);

            await _unitOfWork.Save();

            _outputPort.Ok(createdOrder);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
