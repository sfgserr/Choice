using Choice.Ordering.Application.UseCases.CreateOrder;

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
            CreateOrderUseCase sut = new CreateOrderUseCase(
                _fixture.UserService,
                _fixture.OrderRepository,
                _fixture.UnitOfWork);

            CreateOrderPresenter presenter = new();

            sut.SetOutputPort(presenter);

            await sut.Execute(receiverId, orderRequestId, price, prepayment, deadline, enrollmentDate);

            Assert.NotNull(presenter.Order);
        }
    }
}
