using BuildingBlocks.Domain;
using Users.Domain.Users.Clients;

namespace Users.Domain.OrderRequests.Rules
{
    internal class OnlyCreatorCanChangeRequestRule : IBusinessRule
    {
        private readonly ClientId _changingClientId;
        private readonly ClientId _clientCreatedId;

        internal OnlyCreatorCanChangeRequestRule(ClientId changingClientId, ClientId clientCreatedId)
        {
            _changingClientId = changingClientId;
            _clientCreatedId = clientCreatedId;
        }

        public bool IsBroken => !_changingClientId.Equals(_clientCreatedId);

        public string Message => "You are not the creator of this order request";
    }
}
