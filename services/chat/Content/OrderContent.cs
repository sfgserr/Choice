using Choice.Chat.Api.Content.Interfaces;
using Choice.Chat.Api.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace Choice.Chat.Api.Content
{
    public class OrderContent : IContent
    {
        public OrderContent(string content)
        {
            Content = content;
        }

        public string Content { get; private set; }

        public object GetContent() =>
            JsonConvert.DeserializeObject<Order>(Content)!;

        public bool Match(string propertyName, object value)
        {
            Order order = (Order)GetContent();

            PropertyInfo? property = order.GetType().GetProperty(propertyName);

            if (property is not null)
            {
                return property.GetValue(order) == value;
            }

            return false;
        }

        public void ChangeContent(Action<object> action)
        {
            Order order = (Order)GetContent();

            action(order);

            Content = JsonConvert.SerializeObject(order);
        }
    }
}
