using Choice.Chat.Api.Content.Interfaces;

namespace Choice.Chat.Api.Content
{
    public class DefaultContent : IContent
    {
        public DefaultContent(string content)
        {
            Content = content;
        }

        public string Content { get; }

        public void ChangeContent(Action<object> action)
        {
            action(Content);
        }

        public bool Match(string propertyName, object value)
        {
            if (value is string s)
            {
                return Content == s;
            }

            return false;
        }

        public object GetContent() => Content;
    }
}
