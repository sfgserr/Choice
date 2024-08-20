using BuildingBlocks.Domain;
using Chat.Domain.ChatUsers;
using Chat.Domain.Messages.Events;
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
            OrderResponseId? resonseId, 
            ChatUserId fromUserId, 
            ChatUserId toUserId, 
            bool isActive, 
            DateTime creationDate)
        {
            CheckRule(new CannotSendMessageToYourselfRule(fromUserId, toUserId));
            CheckRule(new OrderResponseIdMustBeProvidedIfTypeOrderRule(type, resonseId));
            CheckRule(new BodyMustBeProvidedIfTypeTextOrImageRule(type, body));

            Id = id;
            Type = type;
            Body = body;
            ResonseId = resonseId;
            FromUserId = fromUserId;
            ToUserId = toUserId;
            IsActive = isActive;
            CreationDate = creationDate;

            AddDomainEvent(new MessageCreatedDomainEvent(Id));
        }

        public static Message CreateTextMessage(
            string text,
            ChatUserId fromUserId,
            ChatUserId toUserId)
        {
            return new Message(
                new(Guid.NewGuid()),
                MessageType.Text,
                text,
                null,
                fromUserId,
                toUserId,
                true,
                DateTime.Now);
        }

        public static Message CreateImageMessage(
            string uri,
            ChatUserId fromUserId,
            ChatUserId toUserId)
        {
            return new Message(
                new(Guid.NewGuid()),
                MessageType.Image,
                uri,
                null,
                fromUserId,
                toUserId,
                true,
                DateTime.Now);
        }

        public static Message CreateOrder(
            OrderResponseId responseId,
            ChatUserId fromUserId,
            ChatUserId toUserId)
        {
            return new Message(
                new(Guid.NewGuid()),
                MessageType.Order,
                null,
                responseId,
                fromUserId,
                toUserId,
                true,
                DateTime.Now);
        }

        public MessageId Id { get; }

        public MessageType Type { get; }

        public string? Body { get; }

        public OrderResponseId? ResonseId { get; }

        public ChatUserId FromUserId { get; }

        public ChatUserId ToUserId { get; }

        public bool IsActive { get; private set; }

        public DateTime CreationDate { get; }
    }
}
