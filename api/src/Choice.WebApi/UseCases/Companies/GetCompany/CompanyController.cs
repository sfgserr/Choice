using Choice.Application.UseCases.Companies.GetCompany;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Companies.GetCompany
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : Controller, IOutputPort
    {
        private readonly IGetCompanyUseCase _useCase;

        private IActionResult _viewModel;

        public CompanyController(IGetCompanyUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.Ok(Company company)
        {
            _viewModel = Ok(company);
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        [HttpGet("{id}/Get")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(id);

            return _viewModel;
        }
    }
}
