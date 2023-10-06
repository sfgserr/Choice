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

        public async Task Execute(User sender, User receiver, string text)
        {
            if (Equals(sender, receiver) || string.IsNullOrEmpty(text))
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(sender, receiver, text);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
