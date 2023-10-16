using Choice.Application.UseCases.Categories.CreateCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [ClassData(typeof(ValidDataSetup))]
        public async Task CreateCategory_Returns_Ok(string title, string iconUri)
        {
            CreateCategoryPresenter presenter = new CreateCategoryPresenter();

            CreateCategoryUseCase sut = new CreateCategoryUseCase(_fixture.CategoryRepositoryFake, _fixture.UnitOfWorkFake);

            sut.SetOutputPort(presenter);

            await sut.Execute(title, iconUri);

            Assert.NotNull(presenter.Category);
        }
    }
}
