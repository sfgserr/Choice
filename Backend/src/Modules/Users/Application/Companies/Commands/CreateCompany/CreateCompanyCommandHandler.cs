using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.GeoCoding;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Companies.Commands.CreateCompany
{
    internal class CreateCompanyCommandHandler : ICommandHandler<CreateCompanyCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUsersCounter _usersCounter;
        private readonly IGeoCodingService _geoCodingService;
        
        internal CreateCompanyCommandHandler(
            IUsersDbContext dbContext, 
            IUsersCounter usersCounter, 
            IGeoCodingService geoCodingService)
        {
            _dbContext = dbContext;
            _usersCounter = usersCounter;
            _geoCodingService = geoCodingService;
        }

        public async Task Execute(CreateCompanyCommand command)
        {
            var coords = await _geoCodingService.GetCoords(command.City, command.Street);
            
            var company = Company.Create(
                command.Name,
                command.Email,
                command.PhoneNumber,
                command.Password,
                command.DeviceName,
                command.DeviceToken,
                new(command.City, command.Street, new(coords[1], coords[0])),
                _usersCounter);

            await _dbContext.Companies.AddAsync(company);
        }
    }
}