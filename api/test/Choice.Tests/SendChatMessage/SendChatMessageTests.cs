using Choice.Application.UseCases.Messages.SendChatMessage;
using Choice.Domain.Models;
using Xunit;

namespace Choice.Tests.SendChatMessage
{
    public sealed class SendChatMessageTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;

        public SendChatMessageTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(ValidDataSetup))]
        public async Task SendChatMessage_Returns_Ok(User sender, Room room, string text)
        {
            SendChatMessagePresenter presenter = new SendChatMessagePresenter();

            SendChatMessageUseCase sut = new SendChatMessageUseCase(_fixture.ChatMessageRepositoryFake, _fixture.UnitOfWorkFake);

            sut.SetOutputPort(presenter);

            await sut.Execute(sender, room, text);

            Assert.NotNull(presenter.Message);
        }

        [Theory]
        [ClassData(typeof(InvalidDataSetup))]
        public async Task SendChatMessage_Returns_Invalid(User sender, Room room, string text)
        {
            SendChatMessagePresenter presenter = new SendChatMessagePresenter();

            SendChatMessageUseCase sendChatMessageUseCase = new SendChatMessageUseCase(_fixture.ChatMessageRepositoryFake, _fixture.UnitOfWorkFake);

            SendChatMessageValidationUseCase sut = new SendChatMessageValidationUseCase(sendChatMessageUseCase);

            sut.SetOutputPort(presenter);

            await sut.Execute(sender, room, text);

            Assert.Null(presenter.Message);
        }
    }
}
