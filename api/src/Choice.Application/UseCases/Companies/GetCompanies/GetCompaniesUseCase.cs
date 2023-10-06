using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.GetCompanies
{
    public class GetCompaniesUseCase : IGetCompaniesUseCase
    {
        private readonly IRepository<Company> _companyRepository;

        private IOutputPort _outputPort;

        public GetCompaniesUseCase(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;

            _outputPort = new GetCompaniesPresenter();
        }

        public async Task Execute() =>
            await GetCompanies();

        private async Task GetCompanies()
        {
            IList<Company> companies = await _companyRepository.Get();

            _outputPort.Ok(companies);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
