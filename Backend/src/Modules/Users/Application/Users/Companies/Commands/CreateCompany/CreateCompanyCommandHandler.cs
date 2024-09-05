using BuildingBlocks.Application.Cqrs.Commands;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Users.Companies.Commands.CreateCompany
{
    internal class CreateCompanyCommandHandler : ICommandHandler<CreateCompanyCommand>
    {
        private readonly ICompanyRepository _repository;
        private readonly IUsersCounter _usersCounter;
        private readonly IGeoService _geoService;
        
        internal CreateCompanyCommandHandler(
            ICompanyRepository repository, 
            IUsersCounter usersCounter, 
            IGeoService geoService)
        {
            _repository = repository;
            _usersCounter = usersCounter;
            _geoService = geoService;
        }

        public async Task Execute(CreateCompanyCommand command)
        {
            string[] coords = await _geoService.GetCoords(command.City, command.Street);
            
            var company = Company.Create(
                command.Name,
                command.Email,
                command.PhoneNumber,
                PasswordManager.HashPassword(command.Password),
                new(command.City, command.Street, new(coords[0], coords[1])),
                _usersCounter);

            await _repository.Add(company);
        }
    }
}