using Choice.Domain.Models;
using Choice.Services.ApiServices;
using System.Threading.Tasks;

namespace Choice.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthentictionService
    {
        private readonly IApiService<Client> _clientApiService;
        private readonly IApiService<Company> _companyApiService;

        public AuthenticationService(IApiService<Client> clientApiService, IApiService<Company> companyApiService)
        {
            _clientApiService = clientApiService;
            _companyApiService = companyApiService;
        }

        public Task LoginByEmail(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task LoginByPhone(string phoneNumber)
        {
            throw new System.NotImplementedException();
        }

        public Task<Client> RegisterClient(string name, string surname, string email, string password, string passwordConfirmtion)
        {
            throw new System.NotImplementedException();
        }

        public Task<Company> RegisterCompany(RegisterCompanyInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
