using BuildingBlocks.Application.Cqrs.Queries;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Users.Companies.Queries.GetCompanyOnMap
{
    internal class GetCompanyOnMapQueryHandler : IQueryHandler<GetCompanyOnMapQuery, CompanyDto>
    {
        private readonly ICompanyRepository _repository;
        private readonly IGeoService _geoService;
        private readonly IUserContext _userContext;
        
        internal GetCompanyOnMapQueryHandler(
            ICompanyRepository repository, 
            IGeoService geoService, 
            IUserContext userContext)
        {
            _repository = repository;
            _geoService = geoService;
            _userContext = userContext;
        }

        public async Task<CompanyDto> Handle(GetCompanyOnMapQuery query)
        {
            var company = await _repository.Get(new(query.CompanyId));

            var distance = await _geoService.GetDistance(_userContext.Address, company.User.Address);

            return new CompanyDto()
            {
                Id = company.Id.Value,
                Name = company.User.Name,
                IconUri = company.User.IconUri,
                Street = company.User.Address.Street,
                City = company.User.Address.City,
                PhotoUris = company.GetPhotoUris(),
                SocialMediaUris = company.GetSocialMediaUris(),
                ReviewsCount = company.User.ReviewsCount,
                AverageGrade = company.User.AverageGrade,
                Distance = distance
            };
        }
    }
}