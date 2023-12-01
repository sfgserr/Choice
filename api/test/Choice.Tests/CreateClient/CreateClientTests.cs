using Choice.Application.UseCases.Clients.CreateClient;
using Xunit;

namespace Choice.UnitTests.CreateClient
{
    public sealed class CreateClientTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;

        public CreateClientTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(ValidDataSetup))]
        public async Task CreateClient_Returns_Ok(string name, string surname, string password, string email, string photoUri)
        {
            CreateClientPresenter presenter = new CreateClientPresenter();

            CreateClientUseCase sut = new CreateClientUseCase(_fixture.ClientRepositoryFake, _fixture.UnitOfWorkFake);

            sut.SetOutputPort(presenter);

            await sut.Execute(name, surname, password, email, photoUri);

            Assert.NotNull(presenter.Client);
        }

        [Theory]
        [ClassData(typeof(InvalidDataSetup))]
        public async Task CreateClient_Returns_Invalid(string name, string surname, string password, string email, string photoUri)
        {
            CreateClientPresenter presenter = new CreateClientPresenter();

            CreateClientUseCase createClientUseCase = new CreateClientUseCase(_fixture.ClientRepositoryFake,
                _fixture.UnitOfWorkFake);

            CreateClientValidationUseCase sut = new CreateClientValidationUseCase(createClientUseCase,
                _fixture.ClientRepositoryFake);

            sut.SetOutputPort(presenter);

            await sut.Execute(name, surname, password, email, photoUri);

            Assert.Null(presenter.Client);
        }
    }
}
