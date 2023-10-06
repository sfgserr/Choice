using Choice.Application.Services;
using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.UpdateCompany
{
    public class UpdateCompanyUseCase : IUpdateCompanyUseCase
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        private IOutputPort _outputPort;

        public UpdateCompanyUseCase(IRepository<Company> companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;

            _outputPort = new UpdateCompanyUseCasePresenter();
        }

        public async Task Execute(Company company)
        {
            Company updatedCompany = await _companyRepository.Update(company);

            await _unitOfWork.Save();

            _outputPort.Ok(updatedCompany);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}
