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

            _outputPort = new SendChatMessageUseCasePresenter();
        }

        public async Task Execute(User sender, User receiver, string text) =>
            await SendChatMessage(sender, receiver, text);

        private async Task SendChatMessage(User sender, User receiver, string text)
        {
            ChatMessage message = new ChatMessage()
            {
                Sender = sender,
                Text = text,
                Receiver = receiver
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
