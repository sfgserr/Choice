using Choice.Chat.Api.Content.Interfaces;
using Choice.Chat.Api.Models;
using Newtonsoft.Json;

namespace Choice.Chat.Api.Content
{
    public class OrderContent : IContent<Order>
    {
        public OrderContent(string content)
        {
            Content = content;
        }

        public string Content { get; }

        public Order GetContent() =>
            JsonConvert.DeserializeObject<Order>(Content);

        public IContent<Order> ChangeContent(Action<Order> action)
        {
            Order order = GetContent();

            action(order);

            return new OrderContent(JsonConvert.SerializeObject(order));
        }
    }
}
