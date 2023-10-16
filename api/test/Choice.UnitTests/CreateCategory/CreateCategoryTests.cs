using Choice.Application.UseCases.Categories.CreateCategory;
using Xunit;

namespace Choice.UnitTests.CreateCategory
{
    public sealed class CreateCategoryTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;

        public CreateCategoryTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        public async Task CreateCategory_Returns_Ok()
        {
            CreateCategoryPresenter presenter = new CreateCategoryPresenter();

            CreateCategoryUseCase sut = new CreateCategoryUseCase(_fixture.CategoryRepositoryFake, _fixture.UnitOfWorkFake);

            sut.SetOutputPort(presenter);

            await sut.Execute("Category", "/photo");

            Assert.NotNull(presenter.Category);
        }
    }
}
