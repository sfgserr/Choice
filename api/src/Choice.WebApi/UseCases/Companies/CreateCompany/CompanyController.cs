using Choice.Application.UseCases.Companies.CreateCompany;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Companies.CreateCompany
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller, IOutputPort
    {
        private readonly ICreateCompanyUseCase _useCase;

        private IActionResult _viewModel;

        public CompanyController(ICreateCompanyUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Company company)
        {
            _viewModel = Ok(company);
        }

        void IOutputPort.Invalid() 
        {
            _viewModel = BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Company company)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(company.Email, company.Password, company.Title, company.PhoneNumber,
                                   company.Address, company.SiteUri, company.SocialMedias,
                                   company.PhotoUris, company.Categories, company.PrepaymentAvailability);

            return _viewModel;
        }
    }
}
