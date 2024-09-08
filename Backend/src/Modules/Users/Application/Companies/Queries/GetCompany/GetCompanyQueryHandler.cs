using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.Users;

namespace Users.Application.Companies.Queries.GetCompany
{
    internal class GetCompanyQueryHandler : IQueryHandler<GetCompanyQuery, CompanyDto>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;

        internal GetCompanyQueryHandler(IUsersDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<CompanyDto> Handle(GetCompanyQuery query)
        {
            var company = await _dbContext.Companies.GetAsNoTracking(c => 
                c.Id.Equals(_userContext.Id));
            
            return new CompanyDto(
                company.Id.Value,
                company.User.Name,
                company.User.Email,
                company.User.PhoneNumber,
                company.Description,
                company.GetPhotoUris(),
                company.GetCategories(),
                company.User.Address.City,
                company.User.Address.Street,
                company.IsPrepaymentAvailable);
        }
    }
}