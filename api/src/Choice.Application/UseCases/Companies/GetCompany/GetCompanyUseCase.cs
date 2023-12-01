using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.GetCompany
{
    public class GetCompanyUseCase : IGetCompanyUseCase
    {
        private readonly IRepository<Company> _companyRepository;

        private IOutputPort _outputPort;

        public GetCompanyUseCase(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;

            _outputPort = new GetCompanyPresenter();
        }

        public async Task Execute(int id)
        {
            Company? company = await _companyRepository.GetBy(c => c.Id == id);

            if (company != null)
            {
                _outputPort.Ok(company);
                return;
            }

            _outputPort.NotFound();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
