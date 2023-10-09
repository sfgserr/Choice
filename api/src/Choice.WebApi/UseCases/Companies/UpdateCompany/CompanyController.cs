using Choice.Application.UseCases.Companies.UpdateCompany;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Companies.UpdateCompany
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller, IOutputPort
    {
        private readonly IUpdateCompanyUseCase _useCase;

        private IActionResult _viewModel;

        public CompanyController(IUpdateCompanyUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Company company)
        {
            _viewModel = Ok(company);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Company company)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(company);

            return _viewModel;
        }
    }
}
