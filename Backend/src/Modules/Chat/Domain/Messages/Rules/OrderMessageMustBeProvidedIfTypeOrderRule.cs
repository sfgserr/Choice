using BuildingBlocks.Domain;
using Chat.Domain.Messages.OrderMessages;

namespace Chat.Domain.Messages.Rules
{
    internal class OrderMessageMustBeProvidedIfTypeOrderRule : IBusinessRule
    {
        private readonly MessageType _messageType;
        private readonly OrderMessage? _orderMessage;

        internal OrderMessageMustBeProvidedIfTypeOrderRule(MessageType messageType, OrderMessage? orderMessage)
        {
            _messageType = messageType;
            _orderMessage = orderMessage;
        }

        public bool IsBroken => _messageType.Equals(MessageType.Order) && _orderMessage is null;

        public string Message { get; } = "Id заказа должно быть указано";
    }
}
