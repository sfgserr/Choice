
using BuildingBlocks.Application.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Payments.Infrastructure.YooKassa.Events.Core 
{
    public static class YooKassaNotifications
    {
        private static readonly Dictionary<string, Type> Notifications = new()
        {
            ["payment.succeeded"] = typeof(PaymentSucceededEvent),
            ["payout.succeeded"] = typeof(PayoutSucceededEvent),
        };
        
        private static readonly Dictionary<string, List<Func<IYooKassaEvent?, Task>>> Handlers = new();

        public static void AddHandler<T>(string name, Func<T?, Task> handler) where T : IYooKassaEvent
        {
            if (!Notifications.TryGetValue(name, out _)) throw new ArgumentException("No such event");
            
            var handlers = Handlers.GetValueOrDefault(name) ?? [];

            handlers.Add(async @event =>  await handler((T)@event));
            
            Handlers[name] = handlers;
        }
        
        public static async Task Handle(string json)
        {
            var jObject = JObject.Parse(json);
            
            var eventName = jObject.SelectToken("event")?.Value<string>() ?? throw new InvalidCommandException(["No such event"]);
            
            foreach (var handler in Handlers[eventName])
            {
                var type = Notifications[eventName];
                
                await handler((IYooKassaEvent)jObject.SelectToken("object")?.ToObject(type));
            }
        }
    }
}