using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.CreateCompany
{
    public class CreateCompanyPresenter : IOutputPort
    {
        public bool IsInvalid { get; set; } = false;
        public Company? Company { get; set; }

        public void Invalid()
        {
            IsInvalid = true;
        }

        public void Ok(Company company)
        {
            Company = company;
        }
    }
}
