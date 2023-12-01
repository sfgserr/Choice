using Choice.Application.UseCases.Companies.CreateCompany;
using Choice.Domain.Models;
using Xunit;

namespace Choice.UnitTests.CreateCompany
{
    public sealed class CreateCompanyTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;

        public CreateCompanyTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(ValidDataSetup))]
        public async Task CreateCompany_Returns_Ok(string email, string password, string title, string phoneNumber, string address, string siteUri, List<SocialMedia> socialMedias, List<string> photoUris, List<Category> categories, PrepaymentAvailability prepaymentAvailability)
        {
            CreateCompanyPresenter presenter = new CreateCompanyPresenter();

            CreateCompanyUseCase sut = new CreateCompanyUseCase(_fixture.CompanyRepositoryFake, _fixture.UnitOfWorkFake);

            sut.SetOutputPort(presenter);

            await sut.Execute(email, password, title, phoneNumber, address, siteUri, socialMedias, photoUris,
                categories, prepaymentAvailability);

            Assert.NotNull(presenter.Company);
        }

        [Theory]
        [ClassData(typeof(InvalidDataSetup))]
        public async Task CreateCompany_Returns_Invalid(string email, string password, string title, string phoneNumber, string address, string siteUri, List<SocialMedia> socialMedias, List<string> photoUris, List<Category> categories, PrepaymentAvailability prepaymentAvailability)
        {
            CreateCompanyPresenter presenter = new CreateCompanyPresenter();

            CreateCompanyUseCase createCompanyUseCase = new CreateCompanyUseCase(_fixture.CompanyRepositoryFake, _fixture.UnitOfWorkFake);

            CreateCompanyValidationUseCase sut = new CreateCompanyValidationUseCase(_fixture.CompanyRepositoryFake, 
                createCompanyUseCase);

            sut.SetOutputPort(presenter);

            await sut.Execute(email, password, title, phoneNumber, address, siteUri, socialMedias, photoUris, 
                categories, prepaymentAvailability);

            Assert.Null(presenter.Company);
        }
    }
}
