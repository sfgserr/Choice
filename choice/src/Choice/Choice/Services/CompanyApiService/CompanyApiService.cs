using Choice.Domain.Models;
using Choice.Services.ApiServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Services.CompanyApiService
{
    public class CompanyApiService
    {
        private readonly IApiService<Company> _companyService;

        public CompanyApiService(IApiService<Company> companyService)
        {
            _companyService = companyService;
        }

        public async Task<Company> Post(Company client)
        {
            return await _companyService.Post("Company/Create", client);
        }

        public async Task<IList<Company>> GetAll()
        {
            return await _companyService.GetAll("Company/Get");
        }

        public async Task<Company> Get(int id)
        {
            return await _companyService.Get($"Client/{id}/Get");
        }

        public async Task<Company> GetByPhone(string phone)
        {
            return await _companyService.Get($"Company/GetByPhoneNumber?phoneNumber={phone}");
        }

        public async Task<Company> Put(Company company)
        {
            return await _companyService.Put("Company/Update", company);
        }
    }
}
