using BuildingBlocks.Application.Cqrs.Queries;

namespace Administration.Application.Queries.GetCompany
{
    public class GetCompanyQuery : IQuery<CompanyDto>
    {
        public GetCompanyQuery(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
    }
}