using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Messages.SendChatMessage
{
    public class SendChatMessageUseCase : ISendChatMessageUseCase
    {
        private readonly IRepository<ChatMessage> _chatMessageRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public SendChatMessageUseCase(IRepository<ChatMessage> chatMessageRepository, IUnitOfWork unitOfWork)
        {
            _chatMessageRepository = chatMessageRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new SendChatMessagePresenter();
        }

        public async Task Execute(User sender, Room room, string text) =>
            await SendChatMessage(sender, room, text);

        private async Task SendChatMessage(User sender, Room room, string text)
        {
            ChatMessage message = new ChatMessage()
            {
                Sender = sender,
                Text = text,
                Room = room
            };

            ChatMessage sentMessage = await _chatMessageRepository.Create(message);

            await _unitOfWork.Save();

            _outputPort.Ok(sentMessage);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
