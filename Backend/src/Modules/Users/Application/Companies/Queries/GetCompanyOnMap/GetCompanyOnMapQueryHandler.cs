using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Companies.Queries.GetCompanyOnMap
{
    internal class GetCompanyOnMapQueryHandler : IQueryHandler<GetCompanyOnMapQuery, CompanyDto>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IGeoService _geoService;
        private readonly IUserContext _userContext;
        
        internal GetCompanyOnMapQueryHandler(
            IUsersDbContext dbContext, 
            IGeoService geoService, 
            IUserContext userContext)
        {
            _dbContext = dbContext;
            _geoService = geoService;
            _userContext = userContext;
        }

        public async Task<CompanyDto> Handle(GetCompanyOnMapQuery query)
        {
            var company = await _dbContext.Companies.GetAsNoTracking(c => 
                c.Id.Equals(new CompanyId(query.CompanyId)));
            
            var distance = _geoService.GetDistance(
                _userContext.Address.Coords, 
                company.User.Address.Coords);

            return new CompanyDto()
            {
                Id = company.Id.Value,
                Name = company.User.Name,
                IconUri = company.User.IconUri,
                Street = company.User.Address.Street,
                City = company.User.Address.City,
                PhotoUris = company.GetPhotoUris(),
		        Description = company.Description,
                SocialMedias = company.GetSocialMedias(),
                ReviewsCount = company.User.ReviewsCount,
                AverageGrade = company.User.AverageGrade,
                Distance = distance
            };
        }
    }
}
