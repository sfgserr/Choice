using Choice.Application.Services;
using Choice.Common.ValueObjects;
using Choice.CompanyService.Api.Entities;
using Choice.CompanyService.Api.Repositories;
using Choice.CompanyService.Api.ViewModels;
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

        public CompanyController(ICompanyRepository repository, IAddressService addressService, IHttpContextAccessor context)
        {
            _repository = repository;
            _addressService = addressService;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            IList<Company> companies = await _repository.GetAll();

            return Ok(companies.Select(c => new CompanyDetailsViewModel(c)));
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string guid, Address address)
        {
            Company company = await _repository.Get(guid);

            if (company is null)
                return NotFound();

            int distance = await _addressService.GetDistance(company.Address, address);

            return Ok(new CompanyViewModel(company, distance));
        }

        [Authorize("Company")]
        [HttpPut("FillCompanyData")]
        public async Task<IActionResult> FillCompanyData(FillCompanyDataRequest request)
        {
            string id = _context.HttpContext?.User.FindFirst("id")?.Value!;

            Company company = await _repository.Get(id);

            if (company is null)
                return NotFound();

            company.FillCompanyData
                (request.SiteUrl,
                 request.Street,
                 request.City,
                 request.SocialMedias,
                 request.PhotoUris,
                 request.CategoriesId);

            bool result = await _repository.Update(company);

            return result ? Ok(company) : BadRequest();
        }
    }
}
