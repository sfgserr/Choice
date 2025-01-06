using BuildingBlocks.Domain;

namespace Chat.Domain.Messages.Rules
{
    internal class BodyMustBeProvidedIfTypeTextOrImageRule : IBusinessRule
    {
        private readonly MessageType _type;
        private readonly string? _body;

        internal BodyMustBeProvidedIfTypeTextOrImageRule(MessageType type, string? body)
        {
            _type = type;
            _body = body;
        }

        public bool IsBroken => !_type.Equals(MessageType.Order) && string.IsNullOrEmpty(_body);

        public string Message { get; } = "Сообщение не может быть пустым";
    }
}
