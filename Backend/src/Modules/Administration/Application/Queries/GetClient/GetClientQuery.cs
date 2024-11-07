using BuildingBlocks.Application.Cqrs.Queries;

namespace Administration.Application.Queries.GetClient
{
    public class GetClientQuery : IQuery<ClientDto>
    {
        public GetClientQuery(Guid clientId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; }
    }
}