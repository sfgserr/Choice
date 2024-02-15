using Choice.Application.UseCases.Companies.CreateCompany;
using Choice.Domain.Models;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.WebApi.UseCases.Companies.CreateCompany
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : Controller, IOutputPort
    {
        private readonly ICreateCompanyUseCase _useCase;
        private readonly IPublishEndpoint _endPoint;

        private IActionResult _viewModel;

        public CompanyController(ICreateCompanyUseCase useCase, IPublishEndpoint endPoint)
        {
            _useCase = useCase;
            _endPoint = endPoint;
        }

        void IOutputPort.Ok(Company company)
        {
            _viewModel = Ok(company);
            _endPoint.Publish(new UserCreatedEvent(company.Id, company.Password, phone: company.PhoneNumber));
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
