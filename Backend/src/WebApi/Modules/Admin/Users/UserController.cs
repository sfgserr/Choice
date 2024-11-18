using Administration.Application.Commands.DeleteClient;
using Administration.Application.Commands.EditClient;
using Administration.Application.Commands.EditCompany;
using Administration.Application.Contracts;
using Administration.Application.Queries.GetClient;
using Administration.Application.Queries.GetClients;
using Administration.Application.Queries.GetCompanies;
using Administration.Application.Queries.GetCompany;
using Identity.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using GetCompanyDto = Administration.Application.Queries.GetCompany.CompanyDto;
using GetClientDto = Administration.Application.Queries.GetClient.ClientDto;
using GetClientsDto = Administration.Application.Queries.GetClients.ClientDto;
using GetCompaniesDto = Administration.Application.Queries.GetCompanies.CompanyDto;

namespace WebApi.Modules.Admin.Users
{
    [ApiController]
    [Route("api/admin/users")]
    public class UserController : Controller
    {
        private readonly IAdministrationModule _adminModule;

        public UserController(IAdministrationModule adminModule)
        {
            _adminModule = adminModule;
        }
        
        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.DeleteClient)]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            await _adminModule.ExecuteCommand(new DeleteClientCommand(id));

            return Ok();
        }
        
        [HttpPut("client")]
        [HasPermission(Permissions.EditClient)]
        public async Task<IActionResult> EditClient(EditClientRequest request)
        {
            await _adminModule.ExecuteCommand(new EditClientCommand(
                request.ClientId,
                request.IconUri,
                request.Name,
                request.PhoneNumber,
                request.Email,
                request.City,
                request.Street));

            return Ok();
        }
        
        [HttpPut("company")]
        [HasPermission(Permissions.EditCompany)]
        public async Task<IActionResult> EditCompany(EditCompanyRequest request)
        {
            await _adminModule.ExecuteCommand(new EditCompanyCommand(
                request.Id,
                request.IconUri,
                request.Name,
                request.Description,
                request.Email,
                request.PhoneNumber,
                request.City,
                request.Street,
                request.SocialMedias,
                request.CategoryIds,
                request.PhotoUris,
                request.IsPrepaymentAvailable));

            return Ok();
        }
        
        [HttpGet("client/{clientId:guid}")]
        [HasPermission(Permissions.GetClientAdmin)]
        public async Task<IActionResult> GetClient(Guid clientId)
        {
            var client = await _adminModule.Query<GetClientQuery, GetClientDto>(new GetClientQuery(clientId));

            return Ok(client);
        }
        
        [HttpGet("company/{companyId:guid}")]
        [HasPermission(Permissions.GetCompanyAdmin)]
        public async Task<IActionResult> GetCompany(Guid companyId)
        {
            var company = await _adminModule.Query<GetCompanyQuery, GetCompanyDto>(new GetCompanyQuery(companyId));

            return Ok(company);
        }
        
        [HttpGet("clients")]
        [HasPermission(Permissions.GetClients)]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _adminModule
                .Query<GetClientsQuery, IEnumerable<GetClientsDto>>(new GetClientsQuery());

            return Ok(clients);
        }
        
        [HttpGet("companies")]
        [HasPermission(Permissions.GetCompaniesAdmin)]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _adminModule
                .Query<GetCompaniesQuery, IEnumerable<GetCompaniesDto>>(new GetCompaniesQuery());

            return Ok(companies);
        }
    }
}