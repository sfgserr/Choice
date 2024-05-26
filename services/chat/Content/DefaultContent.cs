using Choice.Chat.Api.Content.Interfaces;

namespace Choice.Chat.Api.Content
{
    public class DefaultContent : IContent
    {
        public event Action<object>? BodyChanged;

        public DefaultContent(string content)
        {
            Body = content;
        }

        public object Body { get; }

        public void ChangeContent(Action<object> action)
        {
            action(Body);

            BodyChanged?.Invoke(Body);
        }

        public bool Match(string propertyName, object value)
        {
            if (value is string s && Body is string body)
            {
                return body == s;
            }

            return false;
        }
    }
}
