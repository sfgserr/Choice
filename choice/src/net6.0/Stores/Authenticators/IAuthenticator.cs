using Choice.Domain.Models;
using Choice.Services.AuthenticationServices;
using System.Threading.Tasks;

namespace Choice.Stores.Authenticators
{
    public interface IAuthenticator : IStore<User> 
    {
        Task LoginByEmail(string email, string password);

        Task LoginByPhone(string phoneNumber);

        Task RegisterClient(string name, string surname, string email, string password, string passwordConfirmtion);

        Task RegisterCompany(RegisterCompanyInput input);
    }
}
