
namespace BuildingBlocks.Infrastructure.Outbox
{
    public class OutboxMessage
    {
        public OutboxMessage(Guid id, string type, string message, DateTime occuredOn)
        {
            Id = id;
            Type = type;
            Message = message;
            OccuredOn = occuredOn;
        }

        public Guid Id { get; }

        public string Type { get; }

        public string Message { get; }

        public DateTime OccuredOn { get; }

        public DateTime? Processed { get; set; }
    }
}
