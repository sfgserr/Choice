using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.GetCompany
{
    public class GetCompanyUseCasePresenter : IOutputPort
    {
        public bool IsNotFound { get; set; } = false;
        public Company Company { get; set; }

        public void NotFound()
        {
            IsNotFound = true;  
        }

        public void Ok(Company company)
        {
            Company = company;
        }
    }
}
