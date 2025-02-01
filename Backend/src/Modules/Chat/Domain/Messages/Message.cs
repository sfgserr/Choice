using BuildingBlocks.Domain;
using Chat.Domain.ChatUsers;
using Chat.Domain.Messages.Events;
using Chat.Domain.Messages.OrderMessages;
using Chat.Domain.Messages.Rules;

namespace Chat.Domain.Messages
{
    public class Message : Entity, IAggregateRoot
    {
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
            Type = type;
            Body = body;
            OrderMessage = orderMessage;
            FromUserId = fromUserId;
            ToUserId = toUserId;
            CreationDate = creationDate;
            
            AddDomainEvent(new MessageCreatedDomainEvent(Id));
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
        
        public MessageId Id { get; }

        public MessageType Type { get; }

        public string? Body { get; }
        
        public bool IsRead { get; }

        public OrderMessage? OrderMessage { get; }

        public ChatUserId FromUserId { get; }

        public ChatUserId ToUserId { get; }

        public DateTime CreationDate { get; }
    }
}
