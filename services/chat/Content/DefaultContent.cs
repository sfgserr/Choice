using Choice.Chat.Api.Content.Interfaces;

namespace Choice.Chat.Api.Content
{
    public class DefaultContent : IContent
    {
        public event Action<string>? BodyChanged;

        public DefaultContent(string content)
        {
            Body = content;
        }

        public string Body { get; }

        public void ChangeContent(Action<object> action)
        {
            action(Body);

            BodyChanged?.Invoke(Body);
        }

        public bool Match(string propertyName, object value)
        {
            if (value is string s)
            {
                return Body == s;
            }

            return false;
        }

        public object GetContent() => Body;
    }
}
