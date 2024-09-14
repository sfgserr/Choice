namespace BuildingBlocks.Infrastructure.Events
{
    public class IntegrationEventBase
    {
        public IntegrationEventBase(string type, string content)
        {
            Type = type;
            Content = content;
        }

        public string Type { get; }
        
        public string Content { get; }
    }
}