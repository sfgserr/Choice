using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.Categories;
using Users.Domain.Users;

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
            var company = await _dbContext.Companies.Get(c => 
                c.Id.Equals(_userContext.Id));

            var coords = await _geoService.GetCoords(command.City, command.Street);
            
            company.ChangeData(
                command.Name,
                command.Email,
                command.PhoneNumber,
                new(command.City, command.Street, new(coords[0], coords[1])),
                _usersCounter,
                command.Description,
                command.Categories.Select(c => new CategoryId(c)).ToList(),
                command.PhotoUris,
                command.SocialMediaUris,
                command.IsPrepaymentAvailable);
        }
    }
}