using Microsoft.AspNetCore.Mvc;
using Users.Application.Companies.Commands.CreateCompany;
using Users.Application.Contracts;

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
    }
}