using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderRequests.Commands.SetStatus
{
    public class SetStatusCommand : InternalCommandBase
    {
        public SetStatusCommand(Guid id, Guid requestId, string orderStatus) : base(id)
        {
            RequestId = requestId;
            OrderStatus = orderStatus;
        }

        public Guid RequestId { get; }
        
        public string OrderStatus { get; }
    }
}