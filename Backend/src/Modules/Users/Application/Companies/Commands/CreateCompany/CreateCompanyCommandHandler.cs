using BuildingBlocks.Application.Cqrs.Commands;
using Users.Application.Contracts;
using Users.Application.Users;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Companies.Commands.CreateCompany
{
    internal class CreateCompanyCommandHandler : ICommandHandler<CreateCompanyCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUsersCounter _usersCounter;
        private readonly IGeoService _geoService;
        
        internal CreateCompanyCommandHandler(
            IUsersDbContext dbContext, 
            IUsersCounter usersCounter, 
            IGeoService geoService)
        {
            _dbContext = dbContext;
            _usersCounter = usersCounter;
            _geoService = geoService;
        }

        public async Task Execute(CreateCompanyCommand command)
        {
            var coords = await _geoService.GetCoords(command.City, command.Street);
            
            var company = Company.Create(
                command.Name,
                command.Email,
                command.PhoneNumber,
                command.Password,
                new(command.City, command.Street, new(coords[0], coords[1])),
                _usersCounter);

            await _dbContext.Companies.AddAsync(company);
        }
    }
}