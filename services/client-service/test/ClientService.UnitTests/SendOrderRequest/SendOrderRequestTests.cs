using Choice.ClientService.Application.Services;
using Choice.ClientService.Application.UseCases.SendOrderRequest;
using Choice.ClientService.UnitTests;
using Choice.ClientService.UnitTests.SendOrderRequest;

namespace ClientService.UnitTests.SendOrderRequest
{
    public sealed class SendOrderRequestTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;

        public SendOrderRequestTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(ValidDataSetup))]
        public async Task SendOrderRequests_Returns_Ok(string description, List<string> categories, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            SendOrderRequestPresenter presenter = new();

            SendOrderRequestUseCase useCase = new(_fixture.Repository, _fixture.UnitOfWork, _fixture.UserService);

            useCase.SetOutputPort(presenter);

            await useCase.Execute
                (description, 
                 categories,
                 searchRadius,
                 toKnowPrice,
                 toKnowDeadline,
                 toKnowEnrollmentDate);

            Assert.NotNull(presenter.Request);
        }

        [Theory]
        [ClassData(typeof(InvalidDataSetup))]
        public async Task SendOrderRequests_Returns_Invalid(string description, List<string> categories, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            SendOrderRequestPresenter presenter = new();
            Notification notification = new();

            SendOrderRequestUseCase useCase = new(_fixture.Repository, _fixture.UnitOfWork, _fixture.UserService);

            SendOrderRequestValidationUseCase sut = new(useCase, notification);

            sut.SetOutputPort(presenter);

            await sut.Execute
                (description,
                 categories,
                 searchRadius,
                 toKnowPrice,
                 toKnowDeadline,
                 toKnowEnrollmentDate);

            Assert.Null(presenter.Request);
        }
    }
}
