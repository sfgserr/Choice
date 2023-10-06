using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.CreateCompany
{
    public class CreateCompanyUseCase : ICreateCompanyUseCase
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public CreateCompanyUseCase(IRepository<Company> companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new CreateCompanyPresenter();
        }

        public async Task Execute(string email, string password, string title, string phoneNumber, string address, string siteUri, List<SocialMedia> socialMedias, List<string> photoUris, PrepaymentAvailability prepaymentAvailability)
        {
            Company company = new Company()
            {
                Email = email,
                Password = password,
                Title = title,
                PhoneNumber = phoneNumber,
                Address = address,
                SiteUri = siteUri,
                SocialMedias = socialMedias,
                PhotoUris = photoUris,
                PrepaymentAvailability = prepaymentAvailability
            };

            Company createdCompany = await _companyRepository.Create(company);

            await _unitOfWork.Save();

            _outputPort.Ok(createdCompany);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
