using Choice.Ordering.Application.Services;
using Choice.Ordering.Application.UseCases.CreateOrder;
using Ordering.UnitTests.CreateOrder;

namespace Choice.Ordering.UnitTests.CreateOrder
{
    public sealed class CreateOrderTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;

        public CreateOrderTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(ValidDataSetup))]
        public async Task CreateOrder_Returns_Ok(string receiverId, int orderRequestId, double price, double prepayment,
            int deadline, DateTime enrollmentDate)
        {
            ICreateOrderUseCase sut = new CreateOrderUseCase(
                _fixture.UserService,
                _fixture.OrderRepository,
                _fixture.UnitOfWork);

            CreateOrderValidationUseCase useCase = new(sut, new Notification());

            CreateOrderPresenter presenter = new();

            useCase.SetOutputPort(presenter);

            await useCase.Execute(receiverId, orderRequestId, price, prepayment, deadline, enrollmentDate);

            Assert.NotNull(presenter.Order);
        }
    }
}
