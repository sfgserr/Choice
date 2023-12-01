using Choice.Application.UseCases.Companies.GetCompanyByPhoneNumber;
using Choice.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Companies.GetCompanyByPhoneNumber
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller, IOutputPort
    {
        private readonly IGetCompanyByPhoneNumberUseCase _useCase;

        private IActionResult _viewModel;

        public CompanyController(IGetCompanyByPhoneNumberUseCase useCase)
        {
            _useCase = useCase;
        }

        void IOutputPort.NotFound()
        {
            _viewModel = NotFound();
        }

        void IOutputPort.Ok(Company company)
        {
            _viewModel = Ok(company);
        }

        [HttpGet("GetByPhoneNumber")]
        public async Task<IActionResult> GetByPhoneNumber(string phoneNumber)
        {
            _useCase.SetOutputPort(this);

            await _useCase.Execute(phoneNumber);

            return _viewModel;
        }
    }
}
