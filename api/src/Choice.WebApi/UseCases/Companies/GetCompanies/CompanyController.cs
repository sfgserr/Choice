using Choice.Application.UseCases.Companies.GetCompanies;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Companies.GetCompanies
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : Controller, IOutputPort
    {
        private readonly IGetCompaniesUseCase _useCase;

        private IActionResult _viewModel;

        public CompanyController(IGetCompaniesUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(IList<Company> companies)
        {
            _viewModel = Ok(companies);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute();

            return _viewModel;
        }
    }
}
