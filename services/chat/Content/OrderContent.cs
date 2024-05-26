using Choice.Chat.Api.Content.Interfaces;
using Choice.Chat.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Choice.Chat.Api.Content
{
    public class OrderContent : IContent
    {
        public event Action<object>? BodyChanged;

        public OrderContent(object content)
        {
            Body = content;
        }

        public object Body { get; private set; }

        public bool Match(string propertyName, object value)
        {
            Order order = (Order)Body;

            PropertyInfo? property = order.GetType().GetProperty(propertyName);

            if (property is not null)
            {
                return property.GetValue(order) == value;
            }

            return false;
        }

        public void ChangeContent(Action<object> action)
        {
            Order order = (Order)Body;

            action(order);

            Body = JsonConvert.SerializeObject(order);

            BodyChanged?.Invoke(Body);
        }
    }
}
