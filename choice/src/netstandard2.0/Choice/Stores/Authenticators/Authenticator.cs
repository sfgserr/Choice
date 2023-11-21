using Choice.Domain.Models;
using Choice.Services.AuthenticationServices;
using Choice.Services.FileServices;
using System;
using System.Threading.Tasks;

namespace Choice.Stores.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        public event Action StateChanged;

        private readonly IAuthenticationService _authenticationService;
        private readonly IFileService _fileService;

        public Authenticator(IAuthenticationService authentictionService, IFileService fileService)
        {
            _authenticationService = authentictionService;
            _fileService = fileService;
        }

        private User _user;

        public User State
		{
			get => _user;
			set
			{
				_user = value;
				StateChanged?.Invoke();
			}
		}

        public async Task LoginByEmail(string email, string password)
        {
            State = await _authenticationService.LoginByEmail(email, password);
            await _fileService.DownloadPhoto(((Client)State).IconUri);
        }

        public async Task LoginByPhone(string phoneNumber)
        {
            State = await _authenticationService.LoginByPhone(phoneNumber);
        }

        public async Task RegisterClient(string name, string surname, string email, string password, string passwordConfirmtion)
        {
            await _authenticationService.RegisterClient(name, surname, email, password, passwordConfirmtion);
        }

        public async Task RegisterCompany(RegisterCompanyInput input)
        {
            await _authenticationService.RegisterCompany(input);
        }
    }
}
