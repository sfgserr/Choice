using BuildingBlocks.Domain;
using Chat.Domain.Messages;

namespace Chat.Application.Messages.Commands
{
    public class MessageDto
    {
        public MessageDto(
            Guid id, 
            Guid fromUserId, 
            string? body, 
            bool isRead,
            string type, 
            Guid? orderResponseId, 
            DateTime creationDate, 
            DateTime? enrollmentDate, 
            bool? isActive)
        {
            Id = id;
            FromUserId = fromUserId;
            Body = body;
            IsRead = isRead;
            Type = type;
            OrderResponseId = orderResponseId;
            CreationDate = creationDate;
            EnrollmentDate = enrollmentDate;
            IsActive = isActive;
        }

        public Guid Id { get; }
        
        public Guid FromUserId { get; }

        public string? Body { get; }
        
        public bool IsRead { get; }
        
        public string Type { get; }

        public Guid? OrderResponseId { get; }

        public DateTime CreationDate { get; }

        public DateTime? EnrollmentDate { get; }

        public bool? IsActive { get; }
    }
    
    internal static class Extensions
    {
        internal static MessageDto ToDto(this Message message)
        {
            var type = message.GetType();
        
            var fields = type
                .GetFields()
                .Select(field => ExtractValue(field.GetValue(message)))
                .ToArray();

            return (MessageDto)Activator.CreateInstance(typeof(MessageDto), fields)!;
        }

        private static object? ExtractValue(object? obj)
        {
            if (obj == null) return null;

            var objType = obj.GetType();
            
            var valueProperty = objType.GetProperty("Value");
            if (valueProperty != null)
            {
                return valueProperty.GetValue(obj);
            }

            return obj; 
        }
    }
}