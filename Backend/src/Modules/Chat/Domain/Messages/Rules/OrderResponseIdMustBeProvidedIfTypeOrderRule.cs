using BuildingBlocks.Domain;

namespace Chat.Domain.Messages.Rules
{
    internal class OrderResponseIdMustBeProvidedIfTypeOrderRule : IBusinessRule
    {
        private readonly MessageType _messageType;
        private readonly OrderResponseId? _responseId;

        internal OrderResponseIdMustBeProvidedIfTypeOrderRule(MessageType messageType, OrderResponseId? responseId)
        {
            _messageType = messageType;
            _responseId = responseId;
        }

        public bool IsBroken => _messageType.Equals(MessageType.Order) && _responseId is null;

        public string Message { get; } = "Response id must be provided";
    }
}
