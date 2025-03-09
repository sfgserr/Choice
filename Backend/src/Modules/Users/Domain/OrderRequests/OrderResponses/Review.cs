using BuildingBlocks.Domain;
using Users.Domain.OrderRequests.OrderResponses.Events;
using Users.Domain.Users;

namespace Users.Domain.OrderRequests.OrderResponses
{
    public class Review : Entity
    {
        private string _text;

        private int _grade;
        
        private Review(
            OrderResponseId responseId,
            UserId authorId,
            UserId toUserId,
            string text,
            int grade)
        {
            ResponseId = responseId;
            AuthorId = authorId;
            ToUserId = toUserId;
            
            _text = text;
            _grade = grade;
        }

        internal static Review Create(
            OrderResponseId responseId,
            UserId authorId,
            UserId toUserId,
            string text,
            int grade)
        {
            return new Review(
                responseId,
                authorId,
                toUserId,
                text,
                grade > 5 ? 5 : grade < 1 ? 1 : grade);
        }

        public OrderResponseId ResponseId { get; }

        public UserId AuthorId { get; }

        public UserId ToUserId { get; }
    }
}
