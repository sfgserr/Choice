using BuildingBlocks.Domain;
using Chat.Domain.ChatUsers;

namespace Chat.Domain.Messages.Rules
{
    internal class CannotSendMessageToYourselfRule : IBusinessRule
    {
        private readonly ChatUserId _fromUserId;
        private readonly ChatUserId _toUserId;

        internal CannotSendMessageToYourselfRule(ChatUserId fromUserId, ChatUserId toUserId)
        {
            _fromUserId = fromUserId;
            _toUserId = toUserId;
        }

        public bool IsBroken => _fromUserId.Equals(_toUserId);

        public string Message { get; } = "You can't send messages to yourself";
    }
}
