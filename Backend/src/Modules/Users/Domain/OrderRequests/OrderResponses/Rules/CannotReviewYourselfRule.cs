using BuildingBlocks.Domain;
using Users.Domain.Users;

namespace Users.Domain.OrderRequests.OrderResponses.Rules
{
    internal class CannotReviewYourselfRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _toUserId;

        internal CannotReviewYourselfRule(UserId authorId, UserId toUserId)
        {
            _authorId = authorId;
            _toUserId = toUserId;
        }

        public bool IsBroken => _authorId.Equals(_toUserId);

        public string Message { get; } = "You cannot review yourself";
    }
}
