using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Users.Companies.Queries.GetCompany
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
            var company = await _dbContext.Companies.AsNoTracking().FirstOrDefaultAsync(c => 
                c.Id.Equals(_userContext.Id));

            if (company == null) throw new InvalidCommandException(["Company is not found"]);
            
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