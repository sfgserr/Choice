
namespace BuildingBlocks.Infrastructure.InternalCommands
{
    public class InternalCommand
    {
        public InternalCommand(Guid id, string type, string data, DateTime? processedDate, string? error)
        {
            Id = id;
            Type = type;
            Data = data;
            ProcessedDate = processedDate;
            Error = error;
        }

        public Guid Id { get; }

        public string Type { get; }

        public string Data { get; }

        public DateTime? ProcessedDate { get; set; }

        public string? Error { get; set; }
    }
}
