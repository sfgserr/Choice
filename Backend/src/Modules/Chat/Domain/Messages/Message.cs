using BuildingBlocks.Domain;
using Chat.Domain.ChatUsers;
using Chat.Domain.Messages.OrderMessages;
using Chat.Domain.Messages.Rules;

namespace Chat.Domain.Messages
{
    public class Message : Entity, IAggregateRoot
    {
        private MessageType _type;

        private string? _body;

        private bool _isRead;

        private OrderMessage? _orderMessage;

        private ChatUserId _fromUserId;

        private ChatUserId _toUserId;

        private DateTime _creationDate;
        
        private Message()
        {

        }

        private Message(
            MessageId id, 
            MessageType type, 
            string? body,
            OrderMessage? orderMessage, 
            ChatUserId fromUserId, 
            ChatUserId toUserId, 
            DateTime creationDate)
        {
            CheckRule(new CannotSendMessageToYourselfRule(fromUserId, toUserId));
            CheckRule(new OrderMessageMustBeProvidedIfTypeOrderRule(type, orderMessage));
            CheckRule(new BodyMustBeProvidedIfTypeTextOrImageRule(type, body));

            Id = id;
            
            _type = type;
            _body = body;
            _orderMessage = orderMessage;
            _fromUserId = fromUserId;
            _toUserId = toUserId;
            _creationDate = creationDate;
        }

        public static Message CreateMessage(
            string text,
            ChatUserId fromUserId,
            ChatUserId toUserId,
            MessageType type)
        {
            return new Message(
                new(Guid.NewGuid()),
                type,
                text,
                null,
                fromUserId,
                toUserId,
                DateTime.UtcNow);
        }

        public static Message CreateOrder(
            OrderResponseId responseId,
            DateTime? enrollmentDate,
            ChatUserId fromUserId,
            ChatUserId toUserId)
        {
            var id = new MessageId(Guid.NewGuid());

            return new Message(
                id,
                MessageType.Order,
                null,
                OrderMessage.Create(responseId, id, enrollmentDate),
                fromUserId,
                toUserId,
                DateTime.UtcNow);
        }

        public Message ChangeEnrollmentDate(DateTime enrollmentDate)
        {
            CheckRule(new CannotChangeEnrollmentDateOnlyIfOrderMessageRule(_type));
            
            _orderMessage!.SetAsInactive();
            
            return CreateOrder(
                _orderMessage!.ResponseId,
                enrollmentDate,
                _toUserId,
                _fromUserId);
        }
        
        public MessageId Id { get; }
    }
}
