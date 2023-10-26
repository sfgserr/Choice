using Choice.Domain.Models;
using Choice.Services.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                throw new ArgumentException();

            if (client.Password != password)
                throw new ArgumentException();

            return client;
        }

        public async Task<Company> LoginByPhone(string phoneNumber)
        {
            Company company = await _companyApiService.Get($"Company/GetByPhone?phone={phoneNumber}");

            if (company is null)
                throw new ArgumentException();

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

            if (client != null)
                throw new ArgumentException();

            if (client.Password != passwordConfirmtion)
                throw new ArgumentException();

            await _clientApiService.Post("Client/Create", client);
        }

        public async Task RegisterCompany(RegisterCompanyInput input)
        {
            IList<Company> companies = await _companyApiService.GetAll("Company/Get");

            Company companyGotByPhone = companies.FirstOrDefault(c => c.PhoneNumber == input.PhoneNumber);

            if (companyGotByPhone != null)
                throw new ArgumentException();

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
