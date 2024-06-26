﻿using Choice.Chat.Api.Content.Interfaces;

namespace Choice.Chat.Api.Content
{
    public class DefaultContent : IContent
    {
        public event Action<string>? BodyChanged;

        public DefaultContent(string content)
        {
            Body = content;
        }

        public string Body { get; private set; }

        public object GetContent() => Body;

        public void ChangeContent(Func<object, string> action)
        {
            Body = action(Body);

            BodyChanged?.Invoke(Body);
        }

        public bool Match(string propertyName, object value)
        {
            if (string.IsNullOrEmpty(propertyName) && value is string s)
            {
                return Body == s;
            }

            return false;
        }
    }
}
