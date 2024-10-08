using BuildingBlocks.Application.Cqrs.Commands;
using System.Text.Json.Serialization;

namespace Users.Application.OrderRequests.Commands.SetStatus
{
    public class SetStatusCommand : InternalCommandBase
    {
        [JsonConstructor]
        public SetStatusCommand(Guid id, Guid requestId, string orderStatus) : base(id)
        {
            RequestId = requestId;
            OrderStatus = orderStatus;
        }

        internal Guid RequestId { get; }
        
        internal string OrderStatus { get; }
    }
}