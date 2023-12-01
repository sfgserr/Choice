using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.OrderMessages.GetOrderMessages
{
    public class GetOrderMessagesUseCase : IGetOrderMessagesUseCase
    {
        private readonly IRepository<OrderMessage> _orderMessageRepository;

        private IOutputPort _outputPort;

        public GetOrderMessagesUseCase(IRepository<OrderMessage> orderMessageRepository)
        {
            _orderMessageRepository = orderMessageRepository;

            _outputPort = new GetOrderMessagesPresenter();
        }

        public async Task Execute(int user1Id, int user2Id) =>
            await GetOrderMessages(user1Id, user2Id);

        private async Task GetOrderMessages(int user1Id, int user2Id)
        {
            IList<OrderMessage> orderMessages = await _orderMessageRepository.Get();
            orderMessages = orderMessages.Where(o => o.Sender.Id == user1Id || o.Room.Id == user1Id &&
                                                o.Sender.Id == user2Id || o.Room.Id == user2Id).ToList();

            _outputPort.Ok(orderMessages);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
