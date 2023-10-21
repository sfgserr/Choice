﻿using Choice.Application.UseCases.Categories.CreateCategory;
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

        [Theory]
        [ClassData(typeof(InvalidDataSetup))]
        public async Task CreateCategory_Returns_Invalid(string title, string iconUri)
        {
            CreateCategoryPresenter presenter = new CreateCategoryPresenter();

            CreateCategoryUseCase createCategoryUseCase = new CreateCategoryUseCase(_fixture.CategoryRepositoryFake, _fixture.UnitOfWorkFake);

            CreateCategoryValidationUseCase sut = new CreateCategoryValidationUseCase(createCategoryUseCase);

            sut.SetOutputPort(presenter);

            await sut.Execute(title, iconUri);

            Assert.Null(presenter.Category);
        }
    }
}