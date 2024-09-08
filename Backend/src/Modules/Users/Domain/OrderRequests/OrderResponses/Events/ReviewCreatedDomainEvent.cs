using BuildingBlocks.Domain;
using Users.Domain.Users;

namespace Users.Domain.OrderRequests.OrderResponses.Events
{
    public class ReviewCreatedDomainEvent : DomainEventBase
    {
        public ReviewCreatedDomainEvent(int grade, UserId toUserId)
        {
            Grade = grade;
            ToUserId = toUserId;
        }

        public int Grade { get; }
        
        public UserId ToUserId { get; }
    }
}