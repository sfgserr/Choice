using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderRequests.Commands.Enroll
{
    public class EnrollCommand : InternalCommandBase
    {
        public EnrollCommand(Guid id, Guid requestId) : base(id)
        {
            RequestId = requestId;
        }
        
        public Guid RequestId { get; }
    }
}