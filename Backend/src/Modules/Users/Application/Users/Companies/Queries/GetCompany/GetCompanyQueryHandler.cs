using BuildingBlocks.Application.Cqrs.Queries;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Users.Companies.Queries.GetCompany
{
    internal class GetCompanyQueryHandler : IQueryHandler<GetCompanyQuery, CompanyDto>
    {
        private readonly ICompanyRepository _repository;
        private readonly IUserContext _userContext;

        internal GetCompanyQueryHandler(ICompanyRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task<CompanyDto> Handle(GetCompanyQuery query)
        {
            var company = await _repository.Get(new(_userContext.Id.Value));

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