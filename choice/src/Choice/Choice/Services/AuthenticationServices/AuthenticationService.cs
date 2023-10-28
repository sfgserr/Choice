using Choice.Domain.Models;
using Choice.Exceptions;
using Choice.Services.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace Choice.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IApiService<Client> _clientApiService;
        private readonly IApiService<Company> _companyApiService;

        public AuthenticationService(IApiService<Client> clientApiService, IApiService<Company> companyApiService)
        {
            _clientApiService = clientApiService;
            _companyApiService = companyApiService;
        }

        public async Task<Client> LoginByEmail(string email, string password)
        {
            Client client = await _clientApiService.Get($"Client/GetByEmail?email={email}");

            if (client is null)
                throw new UserNotFoundException();

            if (client.Password != password)
                throw new UserNotFoundException();

            return client;
        }

        public async Task<Company> LoginByPhone(string phoneNumber)
        {
            Company company = await _companyApiService.Get($"Company/GetByPhoneNumber?phoneNumber={phoneNumber}");

            if (company is null)
                throw new UserNotFoundByPhoneNumberException();

            VerificationResource.Create(to: $"+7{phoneNumber}", channel: "sms", pathServiceSid: "VAXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            return company;
        }

        public async Task RegisterClient(string name, string surname, string email, string password, string passwordConfirmtion)
        {
            Client client = new Client()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password,
            };

            Client clientGotByEmail = await _clientApiService.Get($"Client/GetByEmail?email={email}");

            if (clientGotByEmail != null)
                throw new EmailAlreadyRegisteredException();

            if (password != passwordConfirmtion)
                throw new PasswordDoesNotEqualToConfirmtionException();

            await _clientApiService.Post("Client/Create", client);
        }

        public async Task RegisterCompany(RegisterCompanyInput input)
        {
            IList<Company> companies = await _companyApiService.GetAll("Company/Get");

            Company companyGotByPhone = companies.FirstOrDefault(c => c.PhoneNumber == input.PhoneNumber);

            if (companyGotByPhone != null)
                throw new PhoneNumberAlreadyRegisteredException();

            Company companyGotByEmail = companies.FirstOrDefault(c => c.Email == input.Email);

            Company company = new Company()
            {
                Address = input.Address,
                PrepaymentAvailability = input.PrepaymentAvailability,
                Categories = input.Categories,
                Email = input.Email,
                Password = input.Password,
                PhoneNumber = input.PhoneNumber,
                PhotoUris = input.PhotoUris,
                SiteUri = input.SiteUri,
                SocialMedias = input.SocialMedias,
                Title = input.Title
            };

            await _companyApiService.Post("Company/Create", company);
        }
    }
}
