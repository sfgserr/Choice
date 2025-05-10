using BuildingBlocks.Domain;
using Users.Domain.Users;

namespace Users.Domain.OrderRequests.OrderResponses.Reviews
{
    public class Review : Entity
    {
        private OrderResponseId _responseId;

        private UserId _authorId;

        private UserId _toUserId;
        
        private string _text;

        private int _grade;
        
        private Review(
            ReviewId id,
            OrderResponseId responseId,
            UserId authorId,
            UserId toUserId,
            string text,
            int grade)
        {
            Id = id;
            
            _responseId = responseId;
            _authorId = authorId;
            _toUserId = toUserId;
            _text = text;
            _grade = grade;
        }
        
        public ReviewId Id { get; }
        
        internal static Review Create(
            OrderResponseId responseId,
            UserId authorId,
            UserId toUserId,
            string text,
            int grade)
        {
            return new Review(
                new(Guid.NewGuid()),
                responseId,
                authorId,
                toUserId,
                text,
                grade > 5 ? 5 : grade < 1 ? 1 : grade);
        }

        internal bool CheckIfReviewed(UserId authorId)
        {
            return _authorId.Equals(authorId);
        }
    }
}
