using Choice.Application.UseCases.Orders.CreateOrder;
using Choice.Domain.Models;
using Xunit;

namespace Choice.UnitTests.CreateOrder
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
        public async Task CreateOrder_Returns_Ok(List<Category> categories, string description, bool toKnowPrice, bool toKnowAppointmentTime, bool toKnowDeadLine, List<string> photoUris, int searchingRadius)
        {
            CreateOrderPresenter presenter = new CreateOrderPresenter();

            CreateOrderUseCase sut = new CreateOrderUseCase(_fixture.OrderRepositoryFake, _fixture.UnitOfWorkFake);

            sut.SetOutputPort(presenter);

            await sut.Execute(categories, description, toKnowPrice, toKnowAppointmentTime, toKnowDeadLine, photoUris, searchingRadius);

            Assert.NotNull(presenter.Order);
        }

        [Theory]
        [ClassData(typeof(InvalidDataSetup))]
        public async Task CreateOrder_Returns_Invalid(List<Category> categories, string description, bool toKnowPrice, bool toKnowAppointmentTime, bool toKnowDeadLine, List<string> photoUris, int searchingRadius)
        {
            CreateOrderPresenter presenter = new CreateOrderPresenter();

            CreateOrderUseCase createOrderUseCase = new CreateOrderUseCase(_fixture.OrderRepositoryFake, _fixture.UnitOfWorkFake);

            CreateOrderValidationUseCase sut = new CreateOrderValidationUseCase(createOrderUseCase);

            sut.SetOutputPort(presenter);

            await sut.Execute(categories, description, toKnowPrice, toKnowAppointmentTime, toKnowDeadLine, photoUris, searchingRadius);

            Assert.Null(presenter.Order);
        }
    }
}
