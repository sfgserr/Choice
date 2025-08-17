using BuildingBlocks.Domain;
using Chat.Domain.ChatUsers;

namespace Chat.Domain.Messages.Rules
{
    internal class UserShouldBeEitherSenderOrReceiverToChangeEnrollmentDateRule : IBusinessRule
    {
        private readonly ChatUserId _fromUserId;
        private readonly ChatUserId _toUserId;
        private readonly ChatUserId _userId;

        internal UserShouldBeEitherSenderOrReceiverToChangeEnrollmentDateRule(ChatUserId fromUserId, ChatUserId toUserId, ChatUserId userId)
        {
            _fromUserId = fromUserId;
            _toUserId = toUserId;
            _userId = userId;
        }

        public bool IsBroken => !_userId.Equals(_toUserId) && !_userId.Equals(_fromUserId);

        public string Message => "Вы не можете отправить сообщение этому пользователю";
    }
}