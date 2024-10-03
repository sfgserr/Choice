using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.Users.Commands.Review
{
    public class ReviewCommand : InternalCommandBase
    {
        public ReviewCommand(Guid id, int grade, Guid toUserId) : base(id)
        {
            Grade = grade;
            ToUserId = toUserId;
        }
        
        internal int Grade { get; }
        
        internal Guid ToUserId { get; }
    }
}