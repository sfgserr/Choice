using Choice.Application.Services;
using Choice.Common.ValueObjects;
using Choice.CompanyService.Api.Entities;
using Choice.CompanyService.Api.Repositories;
using Choice.CompanyService.Api.ViewModels;
using Choice.CompanyService.Api.ViewModels.Requests;
using Choice.EventBus.Messages.Events;
using CompanyService.Api.ViewModels.Requests;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Choice.CompanyService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _repository;
        private readonly IAddressService _addressService;
        private readonly IHttpContextAccessor _context;
        private readonly IPublishEndpoint _endPoint;

        public CompanyController(ICompanyRepository repository, IAddressService addressService, IHttpContextAccessor context, IPublishEndpoint endPoint)
        {
            _repository = repository;
            _addressService = addressService;
            _context = context;
            _endPoint = endPoint;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            IList<Company> companies = await _repository.GetAll();

            return Ok(companies.Where(c => c.IsDataFilled).Select(c => new CompanyDetailsViewModel(c)));
        }

        [HttpGet("GetByCategory")]
        public async Task<IActionResult> GetCompaniesByCategory(int categoryId)
        {
            IList<Company> companies = await _repository.GetAll();

            return Ok(companies.Where(c => c.IsDataFilled && c.CategoriesId.Contains(categoryId))
                .Select(c => new CompanyDetailsViewModel(c)));
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            string id = _context.HttpContext?.User.FindFirst("id")?.Value!;

            Company company = await _repository.Get(id);

            if (company is not null)
                return Ok(company);

            return NotFound();
        }

        [HttpGet("GetCompanyAdmin")]
        [Authorize("Admin")]
        public async Task<IActionResult> GetCompanyAdmin(string guid)
        {
            Company company = await _repository.Get(guid);

            if (company is not null)
                return Ok(company);

            return NotFound();
        }

        [HttpGet("GetCompany")]
        [Authorize("Client")]
        public async Task<IActionResult> GetCompany(string guid)
        {
            string[] addressJson = HttpContext.User.FindFirst("address")?.Value.Split(',')!;

            Address address = new(addressJson[0], addressJson[1]);

            string coords = await _addressService.Geocode(address);

            Company company = await _repository.Get(guid);

            if (company is null)
                return NotFound();

            if (!company.IsDataFilled)
                return NotFound();

            int distance = await _addressService.GetDistance(company.Coordinates, coords);

            return Ok(new CompanyViewModel(company, distance));
        }

        [HttpPut("ChangeData")]
        public async Task<IActionResult> ChangeData(ChangeDataRequest request)
        {
            string id = _context.HttpContext?.User.FindFirst("id")?.Value!;

            Company company = await _repository.Get(id);

            if (company is null)
                return NotFound();

            if (!request.IsValid)
            {
                return BadRequest(new Dictionary<string, string[]>()
                {
                    ["error"] = ["All fields should not be empty"]
                });
            }

            string coords = await _addressService.Geocode(new(request.Street, request.City));

            company.ChangeData
                (request.Title,
                 request.PhoneNumber,
                 request.Email,
                 request.SiteUrl,
                 request.City,
                 request.Street,
                 request.SocialMedias,
                 request.PhotoUris,
                 request.CategoriesId,
                 coords);

            bool result = await _repository.Update(company);

            if (result)
            {
                await _endPoint.Publish<UserDataChangedEvent>(new(
                    company.Guid,
                    company.Title,
                    company.Email,
                    company.PhoneNumber));

                return Ok(new CompanyDetailsViewModel(company));
            }

            return BadRequest();
        }

        [HttpPut("ChangeDataAdmin")]
        [Authorize("Admin")]
        public async Task<IActionResult> ChangeDataAmin(ChangeDataAdminRequest request)
        {
            Company company = await _repository.Get(request.Guid);

            if (company is null)
                return NotFound();

            if (!request.IsValid)
            {
                return BadRequest(new Dictionary<string, string[]>()
                {
                    ["error"] = ["All fields should not be empty"]
                });
            }

            string coords = await _addressService.Geocode(new(request.Street, request.City));

            company.ChangeData
                (request.Title,
                 request.PhoneNumber,
                 request.Email,
                 request.SiteUrl,
                 request.City,
                 request.Street,
                 request.SocialMedias,
                 request.PhotoUris,
                 request.CategoriesId,
                 coords);

            bool result = await _repository.Update(company);

            if (result)
            {
                await _endPoint.Publish<UserDataChangedEvent>(new(
                    company.Guid,
                    company.Title,
                    company.Email,
                    company.PhoneNumber));

                return Ok(new CompanyDetailsViewModel(company));
            }

            return BadRequest();
        }

        [HttpPut("ChangeIconUri")]
        public async Task<IActionResult> ChangeIconUri(string uri)
        {
            string id = _context.HttpContext?.User.FindFirst("id")?.Value!;

            Company company = await _repository.Get(id);

            if (company is null)
                return NotFound();

            company.ChangeIconUri(uri);

            bool result = await _repository.Update(company);

            if (result)
            {
                await _endPoint.Publish<UserIconUriChangedEvent>(new(
                    company.Guid,
                    company.IconUri));

                return Ok(new CompanyDetailsViewModel(company));
            }

            return BadRequest();
        }

        [HttpPut("ChangeIconUriAdmin")]
        [Authorize("Admin")]
        public async Task<IActionResult> ChangeIconUriAdmin(string guid, string uri)
        {
            Company company = await _repository.Get(guid);

            if (company is null)
                return NotFound();

            company.ChangeIconUri(uri);

            bool result = await _repository.Update(company);

            if (result)
            {
                await _endPoint.Publish<UserIconUriChangedEvent>(new(
                    company.Guid,
                    company.IconUri));

                return Ok(new CompanyDetailsViewModel(company));
            }

            return BadRequest();
        }

        [HttpPut("FillCompanyData")]
        public async Task<IActionResult> FillCompanyData(FillCompanyDataRequest request)
        {
            string id = _context.HttpContext?.User.FindFirst("id")?.Value!;

            Company company = await _repository.Get(id);

            if (company is null)
                return NotFound();

            if (!request.IsValid)
            {
                return BadRequest(new Dictionary<string, string[]>() 
                { 
                    ["error"] = ["All fields should not be empty"] 
                });
            }

            company.FillCompanyData
                (request.SiteUrl,
                 request.SocialMedias,
                 request.PhotoUris,
                 request.CategoriesId,
                 request.PrepaymentAvailable);

            bool result = await _repository.Update(company);

            if (result)
            {
                await _endPoint.Publish(new CompanyDataFilledEvent(company.Guid));

                return Ok(company);
            }

            return BadRequest();
        }
    }
}
