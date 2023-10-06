using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.UpdateCompany
{
    public class UpdateCompanyUseCasePresenter : IOutputPort
    {
        public Company Company { get; set; }

        public void Ok(Company company)
        {
            Company = company;
        }
    }
}
