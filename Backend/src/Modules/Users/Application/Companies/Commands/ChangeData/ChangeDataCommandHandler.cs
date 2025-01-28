using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Companies.Commands.ChangeData
{
    internal class ChangeDataCommandHandler : ICommandHandler<ChangeDataCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IUsersCounter _usersCounter;
        private readonly IGeoService _geoService;
        
        internal ChangeDataCommandHandler(
            IUsersDbContext dbContext, 
            IUserContext userContext, 
            IUsersCounter usersCounter, 
            IGeoService geoService)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _usersCounter = usersCounter;
            _geoService = geoService;
        }

        public async Task Execute(ChangeDataCommand command)
        {
            var company = await _dbContext.Companies.Get(c => c.Id.Equals(_userContext.CompanyId));

            var coords = await _geoService.GetCoords(command.City, command.Street);
            
            company.ChangeData(
                command.Name,
                command.Email,
                command.PhoneNumber,
                new(command.City, command.Street, new(coords[1], coords[0])),
                _usersCounter,
                command.Description,
                command.Categories,
                command.PhotoUris,
                command.SocialMediaUris.Select(SocialMedia.Create).ToList(),
                command.IsPrepaymentAvailable);
        }
    }
}