using BuildingBlocks.Application.Cqrs.Queries;

namespace Users.Application.Companies.Queries.GetCompanies
{
    public class GetCompaniesQuery : IQuery<IList<CompanyDto>>
    {
        public GetCompaniesQuery(int categoryId)
        {
            CategoryId = categoryId;
        }

        public int CategoryId { get; }
    }
}