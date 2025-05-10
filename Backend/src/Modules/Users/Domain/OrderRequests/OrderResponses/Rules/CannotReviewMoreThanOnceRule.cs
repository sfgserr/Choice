using BuildingBlocks.Domain;
using Users.Domain.OrderRequests.OrderResponses.Reviews;
using Users.Domain.Users;

namespace Users.Domain.OrderRequests.OrderResponses.Rules
{
    internal class CannotReviewMoreThanOnceRule : IBusinessRule
    {
        private readonly List<Review> _reviews;
        private readonly UserId _authorId;

        internal CannotReviewMoreThanOnceRule(List<Review> reviews, UserId authorId)
        {
            _reviews = reviews;
            _authorId = authorId;
        }

        public bool IsBroken => _reviews.Any(r => r.CheckIfReviewed(_authorId));

        public string Message => "Вы уже написали отзыв";
    }
}
