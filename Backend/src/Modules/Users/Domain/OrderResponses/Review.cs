using BuildingBlocks.Domain;
using Users.Domain.OrderResponses.Events;
using Users.Domain.Users;

namespace Users.Domain.OrderResponses
{
    public class Review : Entity
    {
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
            Text = text;
            Grade = grade;
            
            AddDomainEvent(new ReviewCreatedDomainEvent(grade, toUserId));
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

        public string Text { get; }

        public int Grade { get; }
    }
}
