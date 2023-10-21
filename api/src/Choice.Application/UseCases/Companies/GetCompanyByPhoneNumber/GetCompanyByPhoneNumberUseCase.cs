using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.GetCompanyByPhoneNumber
{
    public class GetCompanyByPhoneNumberUseCase : IGetCompanyByPhoneNumberUseCase
    {
        private readonly IRepository<Company> _companyRepository;

        private IOutputPort _outputPort;

        public GetCompanyByPhoneNumberUseCase(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;

            _outputPort = new GetCompanyByPhoneNumberPresenter();
        }

        public async Task Execute(string phoneNumber)
        {
            Company company = await _companyRepository.GetBy(c => c.PhoneNumber == phoneNumber);

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
