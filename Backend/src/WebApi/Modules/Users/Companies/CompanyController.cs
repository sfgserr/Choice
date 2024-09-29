using BuildingBlocks.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Companies.Commands.ChangeData;
using Users.Application.Companies.Commands.ChangeIconUri;
using Users.Application.Companies.Commands.CreateCompany;
using Users.Application.Companies.Commands.FillData;
using Users.Application.Companies.Queries.GetCompanies;
using Users.Application.Companies.Queries.GetCompany;
using Users.Application.Contracts;
using GetCompaniesDto = Users.Application.Companies.Queries.GetCompanies.CompanyDto;
using GetCompanyDto = Users.Application.Companies.Queries.GetCompany.CompanyDto;

namespace WebApi.Modules.Users.Companies
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : Controller
    {
        private readonly IUsersModule _usersModule;

        public CompanyController(IUsersModule usersModule)
        {
            _usersModule = usersModule;
        }

        [HttpPost()]
        public async Task<IActionResult> Create(CreateCompanyRequest request)
        {
            await _usersModule.ExecuteCommand(new CreateCompanyCommand(
                request.Name,
                request.Password,
                request.Email,
                request.PhoneNumber,
                request.City,
                request.Street));

            return Ok();
        }

        [HttpPost("{iconUri}")]
        [HasPermission(Permissions.ChangeCompanyIconUri)]
        public async Task<IActionResult> ChangeIconUri(string iconUri)
        {
            await _usersModule.ExecuteCommand(new ChangeIconUriCommand(iconUri));

            return Ok();
        }
        
        [HttpPut()]
        [HasPermission(Permissions.ChangeCompanyData)]
        public async Task<IActionResult> ChangeData(ChangeDataRequest request)
        {
            await _usersModule.ExecuteCommand(new ChangeDataCommand(
                request.Name,
                request.Email,
                request.PhoneNumber,
                request.City,
                request.Street,
                request.Description,
                request.Categories,
                request.PhotoUris,
                request.SocialMediaUris,
                request.IsPrepaymentAvailable));

            return Ok();
        }
        
        [HttpPut("fillData")]
        [HasPermission(Permissions.FillData)]
        public async Task<IActionResult> FillData(FillDataRequest request)
        {
            await _usersModule.ExecuteCommand(new FillDataCommand(
                request.Description,
                request.CategoryIds,
                request.PhotoUris,
                request.SocialMediaUris,
                request.IsPrepaymentAvailable));

            return Ok();
        }
        
        [HttpGet("all")]
        [HasPermission(Permissions.GetCompanies)]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _usersModule
                .Query<GetCompaniesQuery, IList<GetCompaniesDto>>(new GetCompaniesQuery());

            return Ok(companies);
        }
        
        [HttpGet()]
        [HasPermission(Permissions.GetCompany)]
        public async Task<IActionResult> GetCompany()
        {
            var company = await _usersModule.Query<GetCompanyQuery, GetCompanyDto>(new GetCompanyQuery());

            return Ok(company);
        }
        
        [HttpGet("{companyId}")]
        [HasPermission(Permissions.GetCompanyOnMap)]
        public async Task<IActionResult> GetCompanyOnMap(Guid companyId)
        {
            var company = await _usersModule.Query<GetCompanyQuery, GetCompanyDto>(new GetCompanyQuery());

            return Ok(company);
        }
    }
}