using Choice.Domain.Models;

namespace Choice.Application.UseCases.Messages.SendChatMessage
{
    public class SendChatMessageValidationUseCase : ISendChatMessageUseCase
    {
        private readonly ISendChatMessageUseCase _useCase;

        private IOutputPort _outputPort;

        public SendChatMessageValidationUseCase(ISendChatMessageUseCase useCase)
        {
            _useCase = useCase;

            _outputPort = new SendChatMessagePresenter();
        }

        public async Task Execute(User sender, Room room, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(sender, room, text);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
