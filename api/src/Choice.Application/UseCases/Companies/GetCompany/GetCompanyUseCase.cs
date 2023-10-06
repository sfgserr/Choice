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

            _outputPort = new GetCompanyUseCasePresenter();
        }

        public async Task Execute(Func<Company, bool> func)
        {
            Company? company = await _companyRepository.GetBy(func);

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
