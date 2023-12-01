using Choice.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<Client> LoginByEmail(string email, string password);

        Task<Company> LoginByPhone(string phoneNumber);

        Task RegisterClient(string name, string surname, string email, string password, string passwordConfirmtion);

        Task RegisterCompany(RegisterCompanyInput input);
    }
}
