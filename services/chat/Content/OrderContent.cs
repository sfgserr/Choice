using Choice.Chat.Api.Content.Interfaces;
using Choice.Chat.Api.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace Choice.Chat.Api.Content
{
    public class OrderContent : IContent
    {
        public event Action<string>? BodyChanged;

        public OrderContent(string content)
        {
            Body = content;
        }

        public string Body { get; private set; }

        public object GetContent() => JsonConvert.DeserializeObject<Order>(Body);

        public bool Match(string propertyName, object value)
        {
            Order order = JsonConvert.DeserializeObject<Order>(Body)!;

            PropertyInfo? property = order.GetType().GetProperty(propertyName);

            if (property is not null)
            {
                return Equals(property.GetValue(order), value);
            }

            return false;
        }

        public void ChangeContent(Func<object, string> action)
        {
            Order order = JsonConvert.DeserializeObject<Order>(Body)!;

            Body = action(order);

            BodyChanged?.Invoke(Body);
        }
    }
}
