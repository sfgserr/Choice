using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.ChatMessages.GetChatMessages
{
    public class GetChatMessagesUseCase : IGetChatMessagesUseCase
    {
        private readonly IRepository<ChatMessage> _chatMessageRepository;
        private readonly IRepository<OrderMessage> _orderMessageRepository;

        private IOutputPort _outputPort;

        public GetChatMessagesUseCase(IRepository<ChatMessage> chatMessageRepository, IRepository<OrderMessage> orderMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
            _orderMessageRepository = orderMessageRepository;

            _outputPort = new GetChatMessagesPresenter();
        }

        public async Task Execute(int user1Id, int user2Id) =>
            await GetChat(user1Id, user2Id);

        private async Task GetChat(int user1Id, int user2Id)
        {
            IList<ChatMessage> chatMessages = await _chatMessageRepository.Get();
            chatMessages = chatMessages.Where(c => c.Room.Id == user1Id || c.Sender.Id == user1Id &&
                                              c.Sender.Id == user2Id || c.Room.Id == user2Id).ToList();

            _outputPort.Ok(chatMessages);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
