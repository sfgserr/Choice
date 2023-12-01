using Choice.Application.UseCases.OrderMessages.SendOrderMessage;
using Choice.Domain.Models;
using Xunit;

namespace Choice.UnitTests.SendOrderMessage
{
    public sealed class SendOrderMessageTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;

        public SendOrderMessageTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(ValidDataSetup))]
        public async Task SendOrderMessage_Returns_Ok(User sender, Room room, double price, DateTime appointmentTime, int duration, Order order)
        {
            SendOrderMessagePresenter presenter = new SendOrderMessagePresenter();

            SendOrderMessageUseCase sut = new SendOrderMessageUseCase(_fixture.OrderMessageRepositoryFake,
                _fixture.UnitOfWorkFake);

            sut.SetOutputPort(presenter);

            await sut.Execute(sender, room, price, appointmentTime, duration, order);

            Assert.NotNull(presenter.OrderMessage);
        }

        [Theory]
        [ClassData(typeof(InvalidDataSetup))]
        public async Task SendOrderMessage_Returns_Invalid(User sender, Room room, double price, DateTime appointmentTime, int duration, Order order)
        {
            SendOrderMessagePresenter presenter = new SendOrderMessagePresenter();

            SendOrderMessageUseCase sendOrderMessageUseCase = new SendOrderMessageUseCase(
                _fixture.OrderMessageRepositoryFake,
                _fixture.UnitOfWorkFake);

            SendOrderMessageValidationUseCase sut = new SendOrderMessageValidationUseCase(sendOrderMessageUseCase);

            sut.SetOutputPort(presenter);

            await sut.Execute(sender, room, price, appointmentTime, duration, order);

            Assert.Null(presenter.OrderMessage);
        }
    }
}
