using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.CreateCompany
{
    public class CreateCompanyValidationUseCase : ICreateCompanyUseCase
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly ICreateCompanyUseCase _useCase;

        private IOutputPort _outputPort;

        public CreateCompanyValidationUseCase(IRepository<Company> companyRepository, ICreateCompanyUseCase useCase)
        {
            _companyRepository = companyRepository;
            _useCase = useCase;

            _outputPort = new CreateCompanyUseCasePresenter();
        }

        public async Task Execute(string email, string password, string title, string phoneNumber, string address, string siteUri, List<SocialMedia> socialMedias, List<string> photoUris, PrepaymentAvailability prepaymentAvailability)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(title) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(siteUri) ||
                photoUris.Count == 0)
            {
                _outputPort.Invalid();
                return;
            }

            Company company = new Company()
            {
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber,
                Address = address,
                SiteUri = siteUri,
                SocialMedias = socialMedias,
                PhotoUris = photoUris,
                PrepaymentAvailability = prepaymentAvailability,
                Title = title
            };

            Company? gotCompanyByEmail = await _companyRepository.GetBy(c => c.Email == company.Email);

            if (gotCompanyByEmail != null)
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(email, password, title, phoneNumber, address, siteUri, socialMedias, photoUris, prepaymentAvailability);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}
