using BuildingBlocks.Domain;
using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;

namespace Users.Domain.OrderRequests.OrderResponses.Rules
{
    internal class OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule : IBusinessRule
    {
        private readonly Guid _userId;
        private readonly CompanyId _companyId;
        private readonly ClientId _clientId;

        internal OnlyClientCreatedOrCompanyResponsedCanMakeOperationsRule(
            Guid userId, 
            CompanyId companyId, 
            ClientId clientId)
        {
            _userId = userId;
            _companyId = companyId;
            _clientId = clientId;
        }

        public bool IsBroken => !_companyId.Equals(new CompanyId(_userId)) && !_clientId.Equals(new ClientId(_userId));

        public string Message { get; } = "You don't have such order";
    }
}
