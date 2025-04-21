namespace Payments.Infrastructure.YooKassa.Events.Core
{
    public class EventObject
    {
        public EventObject(string @event, object @object)
        {
            Event = @event;
            Object = @object;
        }

        public string Event { get; }
        
        public object Object { get; }
    }
}