using BuildingBlocks.Application.Cqrs.Queries;

namespace Users.Application.Users.Companies.Queries.GetCompanyOnMap
{
    public class GetCompanyOnMapQuery : IQuery<CompanyDto>
    {
        public GetCompanyOnMapQuery(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
    }
}