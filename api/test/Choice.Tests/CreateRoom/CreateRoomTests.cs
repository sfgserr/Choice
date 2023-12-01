using Choice.Application.UseCases.Rooms.CreateRoom;
using Xunit;

namespace Choice.UnitTests.CreateRoom
{
    public sealed class CreateRoomTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;

        public CreateRoomTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(ValidDataSetup))]
        public async Task CreateRoom_Returns_Ok(string name)
        {
            CreateRoomPresenter presenter = new CreateRoomPresenter();

            CreateRoomUseCase sut = new CreateRoomUseCase(_fixture.RoomRepositoryFake, _fixture.UnitOfWorkFake);

            sut.SetOutputPort(presenter);

            await sut.Execute(name);

            Assert.NotNull(presenter.Room);
        }

        [Theory]
        [ClassData(typeof(InvalidDataSetup))]
        public async Task CreateRoom_Returns_Invalid(string name)
        {
            CreateRoomPresenter presenter = new CreateRoomPresenter();

            CreateRoomUseCase createRoomUseCase = new CreateRoomUseCase(_fixture.RoomRepositoryFake, _fixture.UnitOfWorkFake);

            CreateRoomValidationUseCase sut = new CreateRoomValidationUseCase(createRoomUseCase);

            sut.SetOutputPort(presenter);

            await sut.Execute(name);

            Assert.Null(presenter.Room);
        }
    }
}
