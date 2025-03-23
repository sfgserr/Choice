using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using BuildingBlocks.Application.GeoCoding;
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
        private readonly IGeoCodingService _geoCodingService;
        
        internal ChangeDataCommandHandler(
            IUsersDbContext dbContext, 
            IUserContext userContext, 
            IUsersCounter usersCounter, 
            IGeoCodingService geoCodingService)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _usersCounter = usersCounter;
            _geoCodingService = geoCodingService;
        }

        public async Task Execute(ChangeDataCommand command)
        {
            var company = await _dbContext.Companies.Get(c => c.Id.Equals(_userContext.CompanyId));

            var coords = await _geoCodingService.GetCoords(command.City, command.Street);
            
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