using BuildingBlocks.Domain;

namespace Chat.Domain.Messages.Rules
{
    internal class CannotChangeEnrollmentDateOnlyIfOrderMessageRule : IBusinessRule
    {
        private readonly MessageType _messageType;

        internal CannotChangeEnrollmentDateOnlyIfOrderMessageRule(MessageType messageType)
        {
            _messageType = messageType;
        }

        public bool IsBroken => !_messageType.Equals(MessageType.Order);

        public string Message => "Сообщение не содержит заказ";
    }
}