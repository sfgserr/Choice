using BuildingBlocks.Domain;
using Users.Domain.Users;

namespace Users.Domain.OrderResponses.Rules
{
    internal class CannotReviewYourselftRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _toUserId;

        internal CannotReviewYourselftRule(UserId authorId, UserId toUserId)
        {
            _authorId = authorId;
            _toUserId = toUserId;
        }

        public bool IsBroken => _authorId.Equals(_toUserId);

        public string Message { get; } = "You cannot review yourself";
    }
}
