using Choice.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Services.AuthenticationServices
{
    public interface IAuthentictionService
    {
        Task LoginByEmail(string email, string password);

        Task LoginByPhone(string phoneNumber);

        Task<Client> RegisterClient(string name, string surname, string email, string password, string passwordConfirmtion);

        Task<Company> RegisterCompany(RegisterCompanyInput input);
    }
}
