
namespace BuildingBlocks.Infrastructure.InternalCommands
{
    public class InternalCommand
    {
        public InternalCommand(Guid id, string type, string data)
        {
            Id = id;
            Type = type;
            Data = data;
        }

        public Guid Id { get; }

        public string Type { get; }

        public string Data { get; }

        public DateTime? Processed { get; set; }

        public string? Error { get; set; }
    }
}
